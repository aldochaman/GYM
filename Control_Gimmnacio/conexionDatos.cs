using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Control_Gimmnacio
{
     class conexionDatos
     {
          private static string cadena = ConfigurationManager.ConnectionStrings["Control_Gimmnacio.Properties.Settings.controlGymConnectionString"].ConnectionString;
          SqlConnection con = new SqlConnection(cadena);
          public DataTable dt = new DataTable();
          public SqlDataAdapter da;
          #region Consulta
          public DataSet consulta(string Qr)
          {
               DataSet ds1 = new DataSet();
               //generamos la conexion


               try
               {
                    con.Open();
                    //ejecuta la instruccion
                    SqlCommand command = new SqlCommand(Qr, con);
                    command.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds1);

               }
               catch (Exception s)
               {
                    // MessageBox.Show(s.Message);
               }
               finally
               {
                    con.Close();
               }
               return ds1;

          }
          /* public SqlDataReader consultaCB(string qr1)
           {
               //generamos la conexion
               SqlConnection conn = new SqlConnection(cadena);
               bool res;
               SqlDataReader daR;
               try
               {
                   conn.Open();
                   //ejecuta la instruccion
                   SqlCommand command = new SqlCommand(qr1, conn);
                  // command.ExecuteReader();
                   daR= command.ExecuteReader();
                   daR.Read();
                   res = true;
               }
               catch (Exception s)
               {
                   // MessageBox.Show(s.Message);
                   res = false;
               }
               finally
               {
                   conn.Close();
               }
               return daR;
           }*/
          #endregion
          #region insertar
          ///<summary>
          ///insersio
          /// </summary>
          public bool insertar(string qr)
          {
               bool res;

               try
               {
                    con.Open();
                    SqlCommand command = new SqlCommand(qr, con);
                    command.ExecuteNonQuery();

                    res = true;
               }
               catch (Exception ex)
               {


                    res = false;
               }
               finally
               {
                    con.Close();
               }
               return res;

          }
          #endregion
          #region update
          //update
          public bool update(string qr1)
          {
               bool res;
               try
               {
                    con.Open();
                    SqlCommand command = new SqlCommand(qr1, con);
                    command.ExecuteNonQuery();

                    res = true;
               }
               catch (Exception ex)
               {

                    res = false;
               }
               finally { con.Close(); }
               return res;

          }
          public bool update(string query, SqlParameter[] parameters)
          {
               bool res;

               try
               {
                    con.Open();
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                    res = true;
               }
               catch (Exception ex)
               {
                    res = false;
                    // Manejar la excepción adecuadamente
               }
               finally
               {
                    con.Close();
               }
               return res;
          }
          #endregion
          #region Eliminar
          public bool eliminar(string sql)
          {
               bool res;
               try
               {
                    con.Open();
                    SqlCommand comm = new SqlCommand(sql, con);
                    comm.ExecuteNonQuery();
                    // MessageBox.Show("Elemento Eliminado", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    res = true;

               }
               catch (Exception ex)
               {
                    // MessageBox.Show("Erro al eliminar" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    res = false;
               }
               finally
               {
                    con.Close();
               }
               return res;

          }

          public DataTable SelectDatatable(string query)
          {
               SqlConnection con = new SqlConnection(cadena);
               dt = new DataTable();
               try
               {
                    con.Open();
                    da = new SqlDataAdapter(query, con);
                    da.Fill(dt);
               }
               catch
               {

               }
               finally
               {
                    con.Close();
               }
               return dt;
          }
          public string ejecutaEscalar(string query)
          {
               try
               {
                    con.Open();
                    SqlCommand command = new SqlCommand(query, con);
                    string valor = Convert.ToString(command.ExecuteScalar());

                    return valor;
               }
               catch (Exception)
               {
                    return "";
               }
               finally
               {
                    con.Close();
               }
          }
          public string GetParametro(string parametro)

          {
               try
               {
                    con.Open();

                    StringBuilder query = new StringBuilder();

                    query.Append("SELECT TOP 1 VALOR FROM XTS_PARAMETROS ");
                    query.Append(" WHERE");
                    query.AppendFormat(" Parametro = '{0}' ", parametro);

                    SqlCommand comando = new SqlCommand(query.ToString(), con);
                    string valor = Convert.ToString(comando.ExecuteScalar());
                    return valor;
               }
               catch (Exception)
               {

                    throw;
               }
               finally
               {
                    con.Close();
               }
          }
          #endregion
     }
}
