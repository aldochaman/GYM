using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_Gimmnacio
{
     public partial class agregaCantMes : Form
     {
          private ToolTip toolTip1;
          public agregaCantMes()
          {
               InitializeComponent();
               //numMes.Value = 1;
               //cmbSocio.Visible = false;
               //checkBoxSocio.Checked = false;
               txtTotal.Text = "00.00";
               txtTotal.TextAlign = HorizontalAlignment.Left;
               llenagridview();
               toolTip1 = new ToolTip();
               toolTip1.OwnerDraw = true;
               toolTip1.Draw += ToolTip1_Draw;
          }

          private void ToolTip1_Draw(object sender, DrawToolTipEventArgs e)
          {
               // Configurar el formato del texto
               Font font = new Font("Arial", 18, FontStyle.Bold); // Fuente, tamaño y estilo
               Brush brush = Brushes.Red; // Color de texto

               // Dibujar el texto
               e.DrawBackground();
               e.Graphics.DrawString(e.ToolTipText, font, brush, e.Bounds.X, e.Bounds.Y);

               // Liberar recursos
               font.Dispose();
               brush.Dispose();
          }

          string q1 = "";
          DataView miFiltro;
          private void llenagridview()
          {
               q1 = "Select idSocio,nombre,estado  from socio left join memsocio on idSocio = idMemS";
               this.miFiltro = dts.consulta(q1).Tables[0].DefaultView;
               dGWSocios.DataSource = miFiltro;
          }

          conexionDatos dts = new conexionDatos();

          private void btnAceptar_Click(object sender, EventArgs e)
          {
               if (dGWSocios.RowCount > 0)
               {
                    if (!string.IsNullOrEmpty(txt_membresia.Text) && txt_estado.Text == "Vigente")
                    {


                         StringBuilder query = new StringBuilder();
                         query.Append("insert into visitante (idsocio,visitante,cantidad,fecha,precio,membresia)");
                         query.AppendFormat("VALUES('{0}','{1}',{2},GETDATE(),{3},{4})", txt_membresia.Text, txt_socio.Text, 1, txtTotal.Text, 1);


                         if (dts.insertar(query.ToString()) == true)
                         {
                              AgregaHistorialVisitas(true);
                              MessageBox.Show("Visita Agregada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              this.Close();
                         }

                    }
                    else
                    {
                         if (txt_membresia.Text.Trim() == "EXE-2024")
                         {
                              if (txt_socio.Text == "") 
                              {
                                   //// Obtener la posición del botón
                                   //int x = txt_socio.Location.X + txt_socio.Width / 2;
                                   //int y = txt_socio.Location.Y + txt_socio.Height / 2;

                                   //// Mostrar el ToolTip en la posición deseada
                                   //toolTip1.Show("Falta el Nombre", this, x, y, 20000); // 2000 indica la duración en milisegundos
                                   MessageBox.Show("Falta ingresar el Nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                              }
                              else 
                              {
                                   StringBuilder query = new StringBuilder();
                                   query.Append("insert into visitante (idsocio,visitante,cantidad,fecha,precio,membresia)");
                                   query.AppendFormat("VALUES('{0}','{1}',{2},GETDATE(),{3},{4})", txt_membresia.Text, txt_socio.Text, 1, txtTotal.Text, 0);
                                   if (dts.insertar(query.ToString()) == true)
                                   {
                                        AgregaHistorialVisitas(false);
                                        MessageBox.Show("Visita Agregada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                   }
                              }
                              
                         }

                    }
               }
               else
               {
                    MessageBox.Show("Selecciona por lo menos un socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }

          }

          private void AgregaHistorialVisitas(bool membresia = false)
          {
               if (membresia)
               {
                    StringBuilder query = new StringBuilder();
                    query.Append("insert into visitante_historial (idsocio,visitante,cantidad,fecha,precio,membresia)");
                    query.AppendFormat("VALUES('{0}','{1}',{2},GETDATE(),{3},{4})", txt_membresia.Text, txt_socio.Text, 1, txtTotal.Text, 1);
                    dts.insertar(query.ToString());
               }
               else
               {
                    StringBuilder query = new StringBuilder();
                    query.Append("insert into visitante_historial (idsocio,visitante,cantidad,fecha,precio,membresia)");
                    query.AppendFormat("VALUES('{0}','{1}',{2},GETDATE(),{3},{4})", txt_membresia.Text, txt_socio.Text, 1, txtTotal.Text, 0);
                    dts.insertar(query.ToString());
               }
          }
          //private void LlenarComboBoxSocios(string nombre)
          //{
          //     StringBuilder q = new StringBuilder();

          //     // Agregar texto al StringBuilder
          //     q.Append("select nombre,idsocio from socio ");
          //     q.Append("INNER JOIN memSocio ON ");
          //     q.Append("socio.idsocio = memSocio.idmems ");
          //     q.Append("where memsocio.estado = 'Vigente'");
          //     if (nombre != "")
          //     {
          //          q.AppendFormat("and {0}", nombre);
          //     }
          //     cmbSocio.DataSource = dts.consulta(q.ToString()).Tables[0];
          //     cmbSocio.DisplayMember = "Nombre";
          //     cmbSocio.ValueMember = "idSocio";
          //}

          //private void checkBoxSocio_CheckedChanged_1(object sender, EventArgs e)
          //{
          //     if (checkBoxSocio.Checked)
          //     {

          //          cmbSocio.Visible = true;
          //          txtNombre.Visible = false;
          //          LlenarComboBoxSocios("");
          //     }
          //     else
          //     {

          //          cmbSocio.Visible = false;
          //          txtNombre.Visible = true;
          //          // Limpiar el ComboBox si es necesario
          //          if (cmbSocio.DataSource == null)
          //          {
          //               cmbSocio.Items.Clear();
          //          }
          //          else
          //          {
          //               // Si tiene un origen de datos, asigna null al origen de datos y luego limpia los elementos
          //               cmbSocio.DataSource = null;
          //               cmbSocio.Items.Clear();
          //          }
          //     }
          //}

          //private void cmbSocio_KeyUp(object sender, KeyEventArgs e)
          //{
          //     string salida = "";
          //     string[] pBusqueda = this.cmbSocio.Text.Split(' ');
          //     foreach (string p in pBusqueda)
          //     {
          //          if (salida.Length == 0)
          //          {
          //               salida = "(Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%')";
          //          }
          //          else
          //          {
          //               salida += "AND (Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%')";
          //          }
          //     }
          //     LlenarComboBoxSocios(salida);
          //     txtTotal.Text = "00.00";
          //}

          private void txtTotal_TextChanged(object sender, EventArgs e)
          {
               txtTotal.TextAlign = HorizontalAlignment.Left;
          }

          private void btn_buscar_Click(object sender, EventArgs e)
          {
               string salida = "";
               string[] pBusqueda = this.txt_socio.Text.Split(' ');
               foreach (string p in pBusqueda)
               {
                    if (salida.Length == 0)
                    {
                         salida = "(Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%' OR idsocio LIKE '%" + p + "%' OR idsocio LIKE '" + p + "%' )";
                    }
                    else
                    {
                         salida += " and (Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%' OR idsocio LIKE '%" + p + "%' OR idsocio LIKE '" + p + "%')";
                    }
               }
               this.miFiltro.RowFilter = salida;
          }

          private void btnSelecionarS_Click(object sender, EventArgs e)
          {
               if (dGWSocios.RowCount > 0)
               {
                    string idMemS = dGWSocios.CurrentRow.Cells[0].Value.ToString();
                    //consultaHistorial(cl);
                    /*consulta de todo*/
                    DataSet dataC = new DataSet();
                    string query = "Select idSocio,estado from socio left join memsocio on idsocio=idMemS where idSocio ='" + idMemS + "'";
                    dataC = dts.consulta(query);
                    DataTable dt2 = dataC.Tables[0];
                    foreach (DataRow row in dt2.Rows)
                    {
                         txt_membresia.Text = row["idsocio"].ToString();
                         txt_estado.Text = row["estado"].ToString();

                         txt_estado.Visible = true;
                         lbl_estado.Visible = true;
                         txt_estado.Enabled = false;
                         lbl_estado.Enabled = true;
                         txt_membresia.Enabled = false;
                    }
                    if (txt_membresia.Text == "EXE-2024")
                    {
                         txtTotal.Text = "30.00";
                    }
                    else
                    {
                         txtTotal.Text = "00.00";
                    }
               }
          }

          private void agregaCantMes_Activated(object sender, EventArgs e)
          {
               txtTotal.Text = "00.00";
          }
     }
}
