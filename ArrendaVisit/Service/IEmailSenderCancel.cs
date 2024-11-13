namespace ArrendaVisit.Service
{
    public interface IEmailSenderCancel
    {
        Task SendEmail(string STRCODIGORESERVA, string? STREMAIL);
    }
}
