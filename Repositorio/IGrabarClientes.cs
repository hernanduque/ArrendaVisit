using Entidades;

namespace Repositorio
{
    public interface IGrabarClientes
    {
        Task<bool> InsertClientes(Models_Clientes_Turnos ClientesTurnos);
        Task<bool> GrabarCita(Models_Clientes_Citas ClientesCitas);
        Task DeleteReserva(string? strcodigoreserva);
        Task ActualizacionVisitaCliente(string? strcodigoreserva, string? estado, string? mensaje);
        


    }
}
