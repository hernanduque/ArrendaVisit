//using Blazorise.States;
using Entidades;
using Repositorio;
//using System.Web.WebPages.Html;


namespace ArrendaVisit.Service
{
    public class GrabarClienteServicio : IgrabarClienteServicio
    {
        private readonly IGrabarClientes _IGrabarClientes;

        private readonly IEmailSender _IEmailSender;

        private readonly IEmailSenderCancel _IEmailSenderCancel;
        public GrabarClienteServicio(IGrabarClientes GrabarClientesparam, IEmailSender emailsender, IEmailSenderCancel emailsenderCancel)
        {
            _IGrabarClientes = GrabarClientesparam;
            _IEmailSender = emailsender;
            _IEmailSenderCancel = emailsenderCancel;
        }

        //---------------------------------------------------------------------------
        public async Task InsertClientes(Models_Clientes_Turnos ClientesTurnos)
        {
            await _IGrabarClientes.InsertClientes(ClientesTurnos);
        }
        public async Task GrabarCita(Models_Clientes_Citas ClientesCitas)
        {
            try
            {
                await _IGrabarClientes.GrabarCita(ClientesCitas);

                try
                {
                    await _IEmailSender.SendEmail(ClientesCitas.fecha, ClientesCitas.strhorareservax, ClientesCitas.Strapartamentovisita, ClientesCitas.nombreasesor, ClientesCitas.stremailcliente, ClientesCitas.STRCODIGORESERVA);
                }
                catch (Exception e)
                {
                    throw e;

                }

            }
            catch (Exception E)
            {

                throw E;
            }
        }
        public async Task DeleteReserva(Models_Parametros objparametros)
        {
            try
            {
                await _IGrabarClientes.DeleteReserva(objparametros.strcodigoasesor);

                await _IEmailSenderCancel.SendEmail(objparametros.strcodigoasesor, objparametros.stremail);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public async  Task ActualizacionVisitaCliente(Models_Parametros objparametros)
        {
            try
            {
                await _IGrabarClientes.ActualizacionVisitaCliente(objparametros.strcodigoasesor, objparametros.strfechareserva, objparametros.stremail);
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }





}
