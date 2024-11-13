using Entidades;
//using Repositorio;

namespace ArrendaVisit.Service
{
    public interface IlistaServicio
    {
        Task<IEnumerable<ModelsTipoPersona>> GetAllTipoPersona();
        Task<IEnumerable<ModelsTipoDocumento>> GetAllTipoDocumento();
        Task<IEnumerable<ModelsApartamentoVisita>> GetAllapartamentovisita();
        Task<IEnumerable<MODELS_ASESOR_APARTAMENTO>> GetAllLAsesorApartamento(string? strcodigolugar);
        Task<IEnumerable<Models_Ocupacion>> GetAllLOCUPACION(Models_Parametros objparametros);
        Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaHorariosReservados(Models_Parametros objparametros);
        Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaServicioCliente();
        Task<ModelsValidacionCliente> GetUser(Models_Parametros objparametros);
        Task<IEnumerable<ModelsRptServicios>> GetServiciosIncumplidos(ModelsObjRptServicio objparametros);
    }
}

