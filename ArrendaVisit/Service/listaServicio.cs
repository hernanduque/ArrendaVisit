using Entidades;
using Repositorio;

namespace ArrendaVisit.Service
{
    public class listaServicio : IlistaServicio

    {
        private readonly ILogger<listaServicio> _logger;
        private readonly IListasRepositorio _IListasRepositorio;
        public listaServicio(IListasRepositorio ListasRepositorio, ILogger<listaServicio> logger)
        {
            _IListasRepositorio = ListasRepositorio;
            _logger = logger;
        }
        public async Task<ModelsValidacionCliente> GetUser(Models_Parametros objparametros)
        {
            return await _IListasRepositorio.GetUser(objparametros);
        }
        public async Task<IEnumerable<ModelsTipoPersona>> GetAllTipoPersona()
        {
           return await _IListasRepositorio.GetAllTipoPersona();
        }
        public async Task<IEnumerable<ModelsTipoDocumento>> GetAllTipoDocumento()
        {
            return await _IListasRepositorio.GetAllTipoDocumento();
        }
       
        public async Task<IEnumerable<ModelsApartamentoVisita>> GetAllapartamentovisita()
        {
            return await _IListasRepositorio.GetAllApartamentoVisita();
        }
        public async Task<IEnumerable<MODELS_ASESOR_APARTAMENTO>> GetAllLAsesorApartamento(string? strcodigoapartamento)
        {
            return await _IListasRepositorio.GetAllLAsesorApartamento(strcodigoapartamento);
        }
        public async Task<IEnumerable<Models_Ocupacion>> GetAllLOCUPACION(Models_Parametros objparametros)
        {
            return await _IListasRepositorio.GetAllLOCUPACION(objparametros);
        }
        public async Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaHorariosReservados(Models_Parametros objparametros)
        {
            return await _IListasRepositorio.GetAllConsultaHorariosReservados(objparametros);
        }

        public async Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaServicioCliente()
        {
            return await _IListasRepositorio.GetAllConsultaServicioCliente();
        }
            public async Task<IEnumerable<ModelsRptServicios>> GetServiciosIncumplidos(ModelsObjRptServicio objservicios)
        {
            return await _IListasRepositorio.GetServiciosIncumplidos(new ModelsObjRptServicio() { FechaIni = objservicios.FechaIni, FechaFin = objservicios.FechaFin });
        }

       
    }
}
