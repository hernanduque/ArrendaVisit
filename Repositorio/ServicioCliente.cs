using Dapper;
//using ProjectBlazorTurnos.Server.Context;
using Entidades;
using System.Data;
//using Radzen;

namespace Repositorio
{
    public class ServicioCliente : IServicioCliente
    {
        private readonly IDbConnection _dbconnection;

        public ServicioCliente(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;

        }
        public async Task<IEnumerable<ModelsCancelaciones>> GetAllConsultaServicioCliente()
        {
            //var sql = @"SELECT t1.strfechareserva,T3.ORDEN,t3.strhorario,t1.strcodigoreserva,t4.STRNOMBREASESOR as STRASESOR,T5.STRAPARTAMENTOVISITA STRAPARTAMENTO,T6.STRNOMBRES + ' ' + T6.STRAPELLIDOS AS STRNOMBRECLIENTE
            //             FROM table_asesor_horario T1 LEFT JOIN table_CLIENTES_CITAS T2 ON T1.strcedulacliente=t2.strcedula 
            //             INNER JOIN TABLE_HORARIOS T3 ON t3.strcodigohorario=t1.strcodigohorario 
            //             INNER JOIN TABLE_ASESOR T4 ON t4.strcodigoasesor=t1.strcodigoasesor 
            //             INNER JOIN TABLE_APARTAMENTO_VISITA T5 ON t5.strcodigo=t1.strcodigoapartamentovisita
            //             INNER JOIN TABLE_CLIENTES_CITAS T6 ON T6.STRCEDULA=T1.STRCEDULACLIENTE
            //             WHERE t1.STRESTADOVISITA=1 
            //             AND TO_NUMBER(SUBSTR(t1.strfechareserva, 1, 4)) =" + DateTime.Now.Date.Year +
            //             " AND SUBSTR(t1.strfechareserva, 6, 2) =" + DateTime.Now.Date.Month +
            //             " AND SUBSTR(t1.strfechareserva, 9, 2)=" + DateTime.Now.Date.Day + " ORDER BY t1.strfechareserva,T3.ORDEN";

            var sql = @"SELECT t1.strfechareserva,T3.ORDEN,t3.strhorario,t1.strcodigoreserva,t4.STRNOMBREASESOR as STRASESOR,T5.STRAPARTAMENTOVISITA STRAPARTAMENTO,T6.STRNOMBRES + ' ' + T6.STRAPELLIDOS AS STRNOMBRECLIENTE
                         FROM table_asesor_horario T1 LEFT JOIN table_CLIENTES_CITAS T2 ON T1.strcedulacliente=t2.strcedula 
                         INNER JOIN TABLE_HORARIOS T3 ON t3.strcodigohorario=t1.strcodigohorario 
                         INNER JOIN TABLE_ASESOR T4 ON t4.strcodigoasesor=t1.strcodigoasesor 
                         INNER JOIN table_apa T5 ON t5.strcodigo=t1.strcodigolugaratencion
                         INNER JOIN TABLE_CLIENTES_CITAS T6 ON T6.STRCEDULA=T1.STRCEDULACLIENTE
                         WHERE t1.STRESTADOVISITA=1 
                         AND CAST(SUBSTRING(t1.strfechareserva, 1, 4) AS INT) =" + DateTime.Now.Date.Year +
                         " AND SUBSTRING(t1.strfechareserva, 6, 2) =" + DateTime.Now.Date.Month +
                         " AND SUBSTRING(t1.strfechareserva, 9, 2) =" + DateTime.Now.Date.Day + " ORDER BY t1.strfechareserva,T3.ORDEN";

            try
            {
                return await _dbconnection.QueryAsync<ModelsCancelaciones>(sql, new { });
            }
            catch (Exception e)
            {

                throw;
            }


        }
    }
}