using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Entidades;
using System.Data;

namespace Repositorio
{
    public class ListaRepositorio : IListasRepositorio
    {
        private readonly IDbConnection _dbconnection;

        public ListaRepositorio(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;

        }
        public async Task<IEnumerable<ModelsTipoPersona>> GetAllTipoPersona()
        {
            var sql = @"select STRCODIGO,STRTIPOPERSONA from Table_Tipo_Persona ORDER BY STRTIPOPERSONA";

            return await _dbconnection.QueryAsync<ModelsTipoPersona>(sql, new { });
        }
        public async Task<IEnumerable<ModelsTipoDocumento>> GetAllTipoDocumento()
        {
            var sql = @"select STRCODIGO,strtipodocumento from Table_Tipo_Documento order by strtipodocumento";

            return await _dbconnection.QueryAsync<ModelsTipoDocumento>(sql, new { });
        }
        public async Task<IEnumerable<ModelsApartamentoVisita>> GetAllApartamentoVisita()
        {
            var sql = @"select STRCODIGO,STRAPARTAMENTOVISITA from TABLE_APARTAMENTO_VISITA WHERE STRESTADO=1 order by STRAPARTAMENTOVISITA";

            return await _dbconnection.QueryAsync<ModelsApartamentoVisita>(sql, new { });
        }
        public async Task<IEnumerable<MODELS_ASESOR_APARTAMENTO>> GetAllLAsesorApartamento(string? strcodigoapartamento)
        {
            var sql = @"SELECT T1.STRCODIGOASESOR,T2.strnombreasesor FROM TABLE_ASESOR_APARTAMENTO T1
                        INNER JOIN TABLE_ASESOR T2 ON t1.strcodigoasesor=t2.strcodigoasesor
                        WHERE T2.STRESTADO=1 AND t1.STRCODIGOASERLUGAR='" + strcodigoapartamento + "'";

            return await _dbconnection.QueryAsync<MODELS_ASESOR_APARTAMENTO>(sql, new { });
        }
        public async Task<IEnumerable<Models_Ocupacion>> GetAllLOCUPACION(Models_Parametros objparametros)
        {
            try
            {
                var sql = @"SELECT s.*  FROM Table_Horarios s  WHERE NOT EXISTS (SELECT * FROM table_asesor_horario a 
                      WHERE s.strcodigohorario = a.strcodigohorario and a.strcodigoasesor='" + objparametros.strcodigoasesor + "' AND a.STRESTADOVISITA<>2 and a.STRESTADOVISITA<>3 and a.STRESTADOVISITA<>4 and a.strfechareserva='" + objparametros.strfechareserva + "') and s.STRESTADO=1 order by s.ORDEN";

                return await _dbconnection.QueryAsync<Models_Ocupacion>(sql, new { });
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public async Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaHorariosReservados(Models_Parametros objparametros)
        {
            var sql = @"SELECT t1.strfechareserva,T3.ORDEN,t3.strhorario,t1.strcodigoreserva,t4.STRNOMBREASESOR as STRASESOR,T5.STRAPARTAMENTOVISTA STAPARTAMENTO
                        FROM table_asesor_horario T1 LEFT JOIN table_CLIENTES_CITAS T2 ON T1.strcedulacliente=t2.strcedula 
                        AND t2.stremail= '" + objparametros.stremail + "' INNER JOIN TABLE_HORARIOS T3 ON t3.strcodigohorario=t1.strcodigohorario INNER JOIN table_asesor T4 ON t4.strcodigoasesor=t1.strcodigoasesor INNER JOIN table_lugar_atencion T5 ON t5.strcodigo=t1.strcodigolugaratencion WHERE t1.STRESTADOATENCION=1 and t1.strcedulacliente= '" + objparametros.strcodigoasesor + "' and t1.strfechareserva >= '" + objparametros.strfechareserva + "' ORDER BY t1.strfechareserva,T3.ORDEN";

            return await _dbconnection.QueryAsync<ModelsCancelaciones>(sql, new { });
        }
        public async Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaServicioCliente()
        {
            var sql = @"SELECT t1.strfechareserva,T3.ORDEN,t3.strhorario,t1.strcodigoreserva,t4.STRNOMBREASESOR as STRASESOR,T5.STRAPARTAMENTOVISITA STRAPARTAMENTO
                         FROM table_asesor_horario T1 LEFT JOIN table_CLIENTES_CITAS T2 ON T1.strcedulacliente=t2.strcedula 
                         INNER JOIN TABLE_HORARIOS T3 ON t3.strcodigohorario=t1.strcodigohorario 
                         INNER JOIN table_asesor T4 ON t4.strcodigoasesor=t1.strcodigoasesor 
                         INNER JOIN table_apartamento_visita T5 ON t5.strcodigo=t1.strcodigoapartamentovisita
                         WHERE t1.STRESTADOVISITA=1  ORDER BY t1.strfechareserva,T3.ORDEN";

            return await _dbconnection.QueryAsync<ModelsCancelaciones>(sql);
        }
        public async Task<ModelsValidacionCliente> GetUser(Models_Parametros objparametros)
        {
            try
            {
                var sql = @"select COUNT(*) AS STRCODIGORESERVA  from table_asesor WHERE strcedula='" + objparametros.strcodigoasesor + "' AND UPPER(stremail)=UPPER('" + objparametros.stremail + "')";

                return await _dbconnection.QueryFirstOrDefaultAsync<ModelsValidacionCliente>(sql, new { });
                //return data;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public async Task<IEnumerable<ModelsRptServicios>> GetServiciosIncumplidos(ModelsObjRptServicio objparametros)
        {
            try
            {
                //var sql = @"SELECT t2.strnombreasesor,t3.strhorario AS HORARIORESERVADO,t1.strfechareserva,t1.strcedulacliente,
                //         trim(UPPER(T4.STRNOMBRES)) || ' ' || trim(UPPER(T4.STRAPELLIDOS)) AS CLIENTE,
                //         t5.strapartamentovisita,t1.strhorareserva,t1.strfechavisita,t1.dtrhorsvisita as horavisita
                //         ,t1.strcodigoreserva,t1.strobservacion,t6.descestado AS ESTADOVISITA
                //         FROM table_asesor_HORARIO T1
                //         INNER JOIN TABLE_ASESOR T2 ON t1.strcodigoasesor = t2.strcodigoasesor
                //         INNER JOIN TABLE_HORARIOS T3 ON t1.strcodigohorario = T3.strcodigohorario
                //         INNER JOIN TABLE_CLIENTES_CITAS T4 ON t1.strcedulacliente = TO_CHAR(T4.STRCEDULA)
                //         INNER JOIN table_apartamento_visita T5 ON t1.strcodigoapatamentovisita = t5.strcodigo
                //         INNER JOIN TABLE_ESTADOS T6 ON t1.strestadovisita = TO_CHAR(t6.strcodigoestado)
                //         where to_char(TO_DATE(t1.strfechareserva,'YYYY/MM/DD'),'YYYYMMDD') BETWEEN '" + objparametros.FechaIni + "' and '" + objparametros.FechaFin +
                //        "' ORDER BY t1.strfechareserva,t6.descestado";

                var sql = @"SELECT t2.strnombreasesor,t3.strhorario AS HORARIORESERVADO,t1.strfechareserva,t1.strcedulacliente,
                         trim(UPPER(T4.STRNOMBRES)) + ' ' + trim(UPPER(T4.STRAPELLIDOS)) AS CLIENTE,
                         t5.strapartamentovisita,t1.strhorareserva,t1.strfechavisita,t1.dtrhorsvisita as horavisita
                         ,t1.strcodigoreserva,t1.strobservacion,t6.descestado AS ESTADOVISITA
                         FROM table_asesor_HORARIO T1
                         INNER JOIN TABLE_ASESOR T2 ON t1.strcodigoasesor = t2.strcodigoasesor
                         INNER JOIN TABLE_HORARIOS T3 ON t1.strcodigohorario = T3.strcodigohorario
                         INNER JOIN TABLE_CLIENTES_CITAS T4 ON t1.strcedulacliente = TO_CHAR(T4.STRCEDULA)
                         INNER JOINTABLE_APARTAMENTO_VISITA T5 ON t1.strcodigoapartamentovisita = t5.strcodigo
                         INNER JOIN TABLE_ESTADOS T6 ON t1.strestadovisita = TO_CHAR(t6.strcodigoestado)
                         where TO_DATE(t1.strfechareserva,'YYYY/MM/DD') BETWEEN TO_DATE('" + objparametros.FechaIni + "','YYYY/MM/DD') and TO_DATE('" + objparametros.FechaFin + "','YYYY/MM/DD')" +
                        " ORDER BY t1.strfechareserva,t6.descestado";

                return await _dbconnection.QueryAsync<ModelsRptServicios>(sql, new { });
            }
            catch (Exception E)
            {

                throw E;
            }

        }

    }
}
