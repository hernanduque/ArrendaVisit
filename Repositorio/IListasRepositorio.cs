using Entidades;

namespace Repositorio
{
    public interface IListasRepositorio
    {
        Task<IEnumerable<ModelsTipoPersona>> GetAllTipoPersona();
        Task<IEnumerable<ModelsTipoDocumento>> GetAllTipoDocumento();
        Task<IEnumerable<ModelsApartamentoVisita>> GetAllApartamentoVisita();
        Task<IEnumerable<MODELS_ASESOR_APARTAMENTO>> GetAllLAsesorApartamento(string? strcodigoapartamento);
        Task<IEnumerable<Models_Ocupacion>> GetAllLOCUPACION(Models_Parametros objparametros);
        Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaHorariosReservados(Models_Parametros objparametros);
        Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaServicioCliente();
        Task<ModelsValidacionCliente> GetUser(Models_Parametros objparametros);
        Task<IEnumerable<ModelsRptServicios>> GetServiciosIncumplidos(ModelsObjRptServicio objparametros);
        //Task<IEnumerable<User>> Login(LoginRequest objparametros);
    }
}
