using Entidades;

namespace ArrendaVisit.Service
{
    public interface IgrabarClienteServicio
    {
        Task InsertClientes(Models_Clientes_Turnos ClientesTurnos);
        Task GrabarCita(Models_Clientes_Citas ClientesCitas);
        Task DeleteReserva(Models_Parametros objparametros);
        Task ActualizacionVisitaCliente(Models_Parametros objparametros);
    }
}
