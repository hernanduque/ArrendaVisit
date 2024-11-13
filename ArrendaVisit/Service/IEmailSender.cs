using System.Threading.Tasks;

namespace ArrendaVisit.Service
{
    public interface IEmailSender
    {
        Task SendEmail(string fecha, string strhorareserva, string StrApartamento, string nombreasesor, string stremailcliente, string STRCODIGORESERVA);
        
    }
}
