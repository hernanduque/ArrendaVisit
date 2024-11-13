using Entidades;


namespace Repositorio
{
    public interface IServicioCliente
    {
        Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaServicioCliente();
    }
}
