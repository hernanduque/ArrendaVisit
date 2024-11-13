using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Repositorio
{
    public class GrabarClientes : IGrabarClientes
    {
        private readonly IDbConnection _dbconnection;

        public GrabarClientes(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }
        public async Task<bool> InsertClientes(Models_Clientes_Turnos ClientesTurnos)
        {
            try
            {
                //LOS ARROBAS SOLO SIRVEN EN SQL SERVER
                //EJEMPLO:
                ///var sql = @"Insert into contactos (ID,FIRTSNAME,LASTNAME,PHONE,ADDRESS) values 
                //(@ID,@FIRTSNAME,@LASTNAME,@PHONE,@ADDRESS)"
                //los dos puntos son para oracle
                //var sql = @"delete from TABLE_CLIENTES_CITAS where STRCEDULA=:STRCEDULA";

                var sql = @"delete from TABLE_CLIENTES_CITAS where STRCEDULA=@STRCEDULA";


                //_dbconnection.Open();

                await _dbconnection.ExecuteAsync(sql, new
                {
                    ClientesTurnos.STRCEDULA
                });


                sql = @"Insert into TABLE_CLIENTES_CITAS (STRCEDULA,STRCODIGOTIPOPERSONA,STRCODIGOTIPODOCUMENTO,STRNOMBRES,STRAPELLIDOS,STRTELEFONOFIJO,STRTELEFONOCELULAR,STREMAIL)
                  values 
                  (@STRCEDULA,@STRCODIGOTIPOPERSONA,@STRCODIGOTIPODOCUMENTO,@STRNOMBRES,@STRAPELLIDOS,@STRTELEFONOFIJO,@STRTELEFONOCELULAR,@STREMAIL)";


                var resulttado = await _dbconnection.ExecuteAsync(sql, new
                {
                    ClientesTurnos.STRCEDULA,
                    ClientesTurnos.STRCODIGOTIPOPERSONA,
                    ClientesTurnos.STRCODIGOTIPODOCUMENTO,
                    ClientesTurnos.STRNOMBRES,
                    ClientesTurnos.STRAPELLIDOS,
                    ClientesTurnos.STRTELEFONOFIJO,
                    ClientesTurnos.STRTELEFONOCELULAR,
                    ClientesTurnos.STREMAIL
                });

                return resulttado > 0;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { _dbconnection.Close(); }
        }

        public async Task<bool> GrabarCita(Models_Clientes_Citas ClientesCitas)
        {
            //var yearsystem = DateTime.Today.Year;
            //var monthsystem = DateTime.Today.Month;
            //var daysystem = DateTime.Today.Day;

            //string fechasistema = yearsystem + "/" + monthsystem.ToString().PadLeft(2, '0') + "/" + daysystem.ToString().PadLeft(2, '0');

            string horasystem = DateTime.Now.ToString("h:mm:ss tt").ToString();
            ClientesCitas.STRFECHAREALVISITA = horasystem;

            try
            {

                string sql = @"Insert into TABLE_ASESOR_HORARIO (STRCODIGOASESOR,STRCODIGOHORARIO,STRFECHARESERVA,STRCEDULACLIENTE,STRCODIGOAPARTAMENTOVISITA,STRESTADOVISITA,STRCODIGORESERVA,STRHORARESERVA)
                          values 
                          (@STRCODIGOASESORASIGNADO,@STRHORARESERVA,@STRFECHARESERVA,@STRCEDULA,@STRCODIGOAPARTAMENTOVISITA,1,@STRCODIGORESERVA,@STRFECHAREALVISITA)";


                var resulttado = await _dbconnection.ExecuteAsync(sql, new
                {
                    ClientesCitas.STRCODIGOASESORASIGNADO,
                    ClientesCitas.STRHORARESERVA,
                    ClientesCitas.STRFECHARESERVA,
                    ClientesCitas.STRCEDULA,
                    ClientesCitas.STRCODIGOAPARTAMENTOVISITA,
                    ClientesCitas.STRCODIGORESERVA,
                    ClientesCitas.STRFECHAREALVISITA
                });

                return resulttado > 0;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { _dbconnection.Close(); }
        }
        public async Task DeleteReserva(string strcodigoreserva)
        {
            var yearsystem = DateTime.Today.Year;
            var monthsystem = DateTime.Today.Month;
            var daysystem = DateTime.Today.Day;

            string fechasistema = yearsystem + "/" + monthsystem.ToString().PadLeft(2, '0') + "/" + daysystem.ToString().PadLeft(2, '0');

            string horasystem = DateTime.Now.ToString("h:mm:ss tt").ToString();

            var sql = @"update table_asesor_horario set STRESTADOVISITA='2',STROBSERVACION='Anulado por el usuario cliente',STRFECHAVISITA='" + fechasistema + "', DTRHORSVISITA='" + horasystem + "' WHERE trim(strcodigoreserva)=trim('" + strcodigoreserva + "')";

            //var sql = @"delete from table_asesor_horario where rtrim(ltrim(strcodigoreserva))=rtrim(ltrim('" + strcodigoreserva + "'))";

            await _dbconnection.ExecuteAsync(sql, new
            {
                strcodigoreserva
            });
        }
        public async Task ActualizacionVisitaCliente(string? strcodigoreserva, string? estado, string? mensaje)
        {
            var yearsystem = DateTime.Today.Year;
            var monthsystem = DateTime.Today.Month;
            var daysystem = DateTime.Today.Day;

            string fechasistema = yearsystem + "/" + monthsystem.ToString().PadLeft(2, '0') + "/" + daysystem.ToString().PadLeft(2, '0');

            string horasystem = DateTime.Now.ToString("h:mm:ss tt").ToString();

            var sql = @"update table_asesor_horario set STRESTADOVISITA='" + estado + "',STROBSERVACION='" + mensaje + "',STRFECHAVISITA='" + fechasistema + "', DTRHORSVISITA='" + horasystem + "' WHERE trim(strcodigoreserva)=trim('" + strcodigoreserva + "')";

            //var sql = @"delete from table_asesor_horario where rtrim(ltrim(strcodigoreserva))=rtrim(ltrim('" + strcodigoreserva + "'))";

            await _dbconnection.ExecuteAsync(sql, new
            {
                strcodigoreserva
            });
        }

      

        

        
    }
}
