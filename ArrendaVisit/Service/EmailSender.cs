using System.Net;
using System.Net.Mail;

//using System.Text;
using System.Web;

namespace ArrendaVisit.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmail(string fecha, string strhorareserva, string StrApartamento, string nombreasesor, string stremailcliente, string STRCODIGORESERVA)
        {
            try
            {
                //var configurationValue = ConfigurationManager.AppSettings["ConfigurationSettingName"];

                MailAddress to = new MailAddress(stremailcliente);
                MailAddress from = new MailAddress(_emailConfig.From, "Solicitud Reservas Citas Visita Apartamento ArrendaVisit");
                using (MailMessage mm = new MailMessage(from, to))
                {
                    MailAddress copy = new MailAddress(_emailConfig.cc);
                    mm.CC.Add(copy);
                    mm.Subject = "Solicitud Reservas Citas Apartamento ArrendaVisit";
                    mm.Body = HttpUtility.HtmlDecode("Señor Usuario, recuerde:<br /><br />1. Presentarse a la hora programada, y si es posible una lista de preguntas como:<br />  &nbsp;&nbsp;&nbsp;&nbsp;• Cercania de colegios.<br />  &nbsp;&nbsp;&nbsp;&nbsp;•Cercania de servicios de salud, transporte y recreacion.<br />  &nbsp;&nbsp;&nbsp;&nbsp;•En caso de ir acompañado procure sea alguien acertivo y de confianza.<br />2. Al momento de llegar al apartamento por favor anunciarse con el portero indicando que tiene una cita programada.</><br />4. Si desea cancelar su cita puede hacerlo a través de la misma plataforma web: <a href='https://evm-wapp04:8080/Cancelar'>De click Aqui</a>  <br /><br />  &nbsp;&nbsp;&nbsp;&nbsp;*Fecha de la cita: " + @fecha + "<br /> &nbsp;&nbsp;&nbsp;&nbsp;*Hora de la cita: " + @strhorareserva + "<br /> &nbsp;&nbsp;&nbsp;&nbsp;*Lugar de la cita: " + @StrApartamento + "<br /> &nbsp;&nbsp;&nbsp;&nbsp;*Creador de experiencia: " + @nombreasesor + "<br /> &nbsp;&nbsp;&nbsp;&nbsp;*Código de la cita: " + @STRCODIGORESERVA + "<br /><br /> Gracias por usar nuestros servicios en línea. <br /><br />");
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
            catch (Exception E)
            {

                throw E;
            }
        }
    }
}
