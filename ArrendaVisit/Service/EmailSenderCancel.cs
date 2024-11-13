using System.Net.Mail;
using System.Net;
using System.Web;

namespace ArrendaVisit.Service
{
    public class EmailSenderCancel : IEmailSenderCancel
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSenderCancel(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmail(string STRCODIGORESERVA, string? stremailcliente)
        {
            MailAddress to = new MailAddress(stremailcliente);
            MailAddress from = new MailAddress(_emailConfig.From, "Alerta del sistema de reservas ArrendaVisit");
            using (MailMessage mm = new MailMessage(from, to))
            {
                MailAddress copy = new MailAddress(_emailConfig.cc);
                mm.CC.Add(copy);
                mm.Subject = "Cancelacion Citas ArriendaVisit";
                mm.Body = HttpUtility.HtmlDecode("Señor Usuario:  <br /><br />  *Usted a cancelado la reserva que tenia para ser atendido por ArriendaVisit: '" + STRCODIGORESERVA + "'<br /><br />");
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = _emailConfig.SmtpServer;
                smtp.EnableSsl = _emailConfig.EnableSsl;
                smtp.Port = _emailConfig.Port;
                smtp.UseDefaultCredentials = _emailConfig.UseDefaultCredentials;
                NetworkCredential networkCred = new NetworkCredential(_emailConfig.UserName, _emailConfig.Password);
                smtp.Credentials = networkCred;

                smtp.Send(mm);
            }
        }
    }
}
