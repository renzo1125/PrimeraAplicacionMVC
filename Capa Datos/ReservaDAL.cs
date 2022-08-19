using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
//Conectarme a base de datos SQL Server
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Capa_Datos
{
	public class ReservaDAL:CadenaDAL
	{

        public int guardarReserva(ReservaCLS oReservaCLSS)
        {
            //error
            //Rpta 0 va a ser error
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspInsertarReserva", cn))
                    {
                        //Indico que es Procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidhabitacion", oReservaCLSS.iidhabitacion);
                        cmd.Parameters.AddWithValue("@fechainicio", oReservaCLSS.fechainicio);
                        cmd.Parameters.AddWithValue("@fechafin", oReservaCLSS.fechafin);
                        cmd.Parameters.AddWithValue("@cantidadnoches", oReservaCLSS.cantidadnoches);
                        cmd.Parameters.AddWithValue("@preciohabitacion", oReservaCLSS.preciohabitacion);
                        cmd.Parameters.AddWithValue("@total", oReservaCLSS.total);
                        cmd.Parameters.AddWithValue("@iidhotel", oReservaCLSS.iidhotel);
                        cmd.Parameters.AddWithValue("@iidusuario", oReservaCLSS.iidusuario);

                        rpta = cmd.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    rpta = 0;
                }


            }
            return rpta;

        }

        public List<ReservaMostrarCLS> listarReservaHabitacion(int iidhabitacion)
        {
            List<ReservaMostrarCLS> lista = null;
            //  string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString; 
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //Abro la conexion
                    cn.Open();
                    //Llame al procedure
                    using (SqlCommand cmd = new SqlCommand("uspReserva", cn))
                    {
                        //Buena practica (Opcional)->Indicamos que es un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IIDHABITACION", iidhabitacion);
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<ReservaMostrarCLS>();
                            ReservaMostrarCLS oReservaMostrarCLS;
                            int posId = drd.GetOrdinal("IIDRESERVA");
                            int posNombre = drd.GetOrdinal("NOMBRECOMPLETO");
                            int posFechaInicio = drd.GetOrdinal("FECHAINICIORESERVA");
                            int posFechaFin = drd.GetOrdinal("FECHAFINRESERVA");

                            while (drd.Read())
                            {
                                oReservaMostrarCLS = new ReservaMostrarCLS();
                                oReservaMostrarCLS.id = drd.IsDBNull(posId) ? 0 :
                                    drd.GetInt32(posId);
                                oReservaMostrarCLS.title = drd.IsDBNull(posNombre) ? ""
                                    : drd.GetString(posNombre);
                                oReservaMostrarCLS.start = drd.IsDBNull(posFechaInicio) ? ""
                                   : drd.GetString(posFechaInicio);
                                oReservaMostrarCLS.end = drd.IsDBNull(posFechaFin) ? ""
                                   : drd.GetString(posFechaFin);

                                lista.Add(oReservaMostrarCLS);
                            }
                        }

                    }

                    //Cierro una vez de traer la data
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                }

            }
            return lista;


        }



    }
}
