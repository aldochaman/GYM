﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Framework.CrystalReports;

namespace Control_Gimmnacio
{
     public partial class vHistorialUs : Form
     {
          string fHoy = "";
          private byte[] buffer;
          public vHistorialUs()
          {
               InitializeComponent();
               btnGuardarS.Enabled = false;
               btnEliminaMemS.Enabled = false;
               btnAgregaMemS.Enabled = false;
               btnActualizar.Enabled = false;
               consultaSocio();
               fHoy = DateTime.Now.ToShortDateString();
               btn_enviar_correo.Visible = false;
          }
          private void vHistorialUs_Load(object sender, EventArgs e)
          {

               txtClaveS.Enabled = false;
          }
          private void pictureBox1_Click(object sender, EventArgs e)
          {
               this.Close();
          }
          #region arrastrar
          // Arrastrar formulario
          [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
          private extern static void ReleaseCapture();
          [DllImport("user32.DLL", EntryPoint = "SendMessage")]
          private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
          private void vHistorialUs_MouseDown(object sender, MouseEventArgs e)
          {
               ReleaseCapture();
               SendMessage(this.Handle, 0x112, 0xf012, 0);

          }

          private void panel1_MouseDown(object sender, MouseEventArgs e)
          {
               ReleaseCapture();
               SendMessage(this.Handle, 0x112, 0xf012, 0);
          }
          #endregion
          conexionDatos dts = new conexionDatos();

          #region consultas
          string q1 = "";
          DataView miFiltro;
          public void consultaSocio()
          {
               q1 = "Select idSocio as Clave, nombre as Nombre,tel as Telefono_de_Contacto, correo as Facebook  from socio";
               this.miFiltro = dts.consulta(q1).Tables[0].DefaultView;
               dGWSocios.DataSource = miFiltro;
          }
          public void consultaSocio2(string c)
          {
               btnGuardarS.Enabled = true;
               btnEliminaMemS.Enabled = true;
               btnAgregaMemS.Enabled = true;
               btnActualizar.Enabled = true;
               /*consulta de todo*/
               DataSet dataC = new DataSet();
               string qCompruena = "Select idSocio as c, nombre as n, fNacimiento as fn, sexo as s,dir as dr, tel as tl, correo as correo  from socio where idSocio ='" + c + "'";
               dataC = dts.consulta(qCompruena);
               DataTable dt2 = dataC.Tables[0];
               string f = "";
               foreach (DataRow row in dt2.Rows)
               {
                    txtClaveS.Text = row["c"].ToString();
                    txtNomS.Text = row["n"].ToString();
                    txtdir.Text = row["dr"].ToString();
                    txtTel.Text = row["tl"].ToString();
                    txtFB.Text = row["correo"].ToString();
                    txtS.Text = row["s"].ToString();
                    f = row["fn"].ToString();
                    DateTime myDateTime = DateTime.Parse(f);
                    string dia = Convert.ToString(myDateTime.Day);
                    string mes = Convert.ToString(myDateTime.Month);
                    string ano = Convert.ToString(myDateTime.Year);
                    dtNacimiento.Value = myDateTime;
               }

          }

          public void consultaHistorial(string clave)
          {
               q1 = "select idControl as ID,idMemS as Clave, tMem as Tipo, fechaIngreso as Fecha_Inicial, fechatermino as Fecha_Final,prom as Promocion, total as Monto_Pagado, estado as Estatus from memSocio where idMems = '" + clave + "' order  by Estatus desc";
               dGWHistorial.DataSource = dts.consulta(q1).Tables[0];
               dGWHistorial.Columns["Monto_Pagado"].DefaultCellStyle.Format = "N2";
               dGWHistorial.Columns["Monto_Pagado"].DefaultCellStyle.NullValue = "0.00";
               dGWHistorial.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
               timer1.Start();
          }
          public void consultaHistorial2(string clave)
          {
               q1 = "select idControl as ID,idMemS as Clave, tMem as Tipo, fechaIngreso as Fecha_Inicial, fechatermino as Fecha_Final,prom as Promocion, total as Monto_Pagado, estado as Estatus from memSocio where idMems = '" + clave + "' order  by Estatus desc";
               dGWHistorial.DataSource = dts.consulta(q1).Tables[0];
               dGWHistorial.Columns["Monto_Pagado"].DefaultCellStyle.Format = "N2";
               dGWHistorial.Columns["Monto_Pagado"].DefaultCellStyle.NullValue = "0.00";
               // timer1.Start();
          }
          #endregion

          private void btnSelecionarS_Click(object sender, EventArgs e)
          {
               btn_enviar_correo.Visible = false;
               if (dGWSocios.RowCount > 0)
               {
                    string cl = dGWSocios.CurrentRow.Cells[0].Value.ToString();
                    consultaHistorial(cl);
                    btnGuardarS.Enabled = true;
                    btnEliminarS.Enabled = false;
                    btnEliminaMemS.Enabled = true;
                    btnAgregaMemS.Enabled = true;
                    btnActualizar.Enabled = true;
                    /*consulta de todo*/
                    DataSet dataC = new DataSet();
                    string qCompruena = "Select idSocio as c, nombre as n, fNacimiento as fn, sexo as s,dir as dr, tel as tl, correo as correo,QrCodePath from socio where idSocio ='" + cl + "'";
                    dataC = dts.consulta(qCompruena);
                    DataTable dt2 = dataC.Tables[0];
                    string f = "";
                    foreach (DataRow row in dt2.Rows)
                    {
                         txtClaveS.Text = row["c"].ToString();
                         txtNomS.Text = row["n"].ToString();
                         txtdir.Text = row["dr"].ToString();
                         txtTel.Text = row["tl"].ToString();
                         txtFB.Text = row["correo"].ToString();
                         txtS.Text = row["s"].ToString();
                         f = row["fn"].ToString();
                         DateTime myDateTime = DateTime.Parse(f);
                         string dia = Convert.ToString(myDateTime.Day);
                         string mes = Convert.ToString(myDateTime.Month);
                         string ano = Convert.ToString(myDateTime.Year);
                         dtNacimiento.Value = myDateTime;
                         /* txtD.Text = dia;
                          txtM.Text = mes;
                          txtAñoF.Text = ano;*/
                         // Ruta de la imagen
                         string rutaImagen = row["QrCodePath"].ToString(); ;

                         // Cargar la imagen desde la ruta especificada
                         Image imagen = Image.FromFile(rutaImagen);

                         // Asignar la imagen al PictureBox
                         pictureBox2.Image = imagen;
                    }
               }

          }
          public void limpiar()
          {
               txtClaveS.Text = "";
               txtNomS.Text = "";
               txtdir.Text = "";
               txtTel.Text = "";
               txtFB.Text = "";
               txtS.Text = "";
               /*  txtD.Text = "";
                 txtM.Text = "";
                 txtAñoF.Text = "";*/
               btnEliminarS.Enabled = true;
               dGWHistorial.DataSource = null;
               btnActualizar.Enabled = false;
               btnGuardarS.Enabled = false;
               btnAgregaMemS.Enabled = false;
               btnEliminaMemS.Enabled = false;
               btn_enviar_correo.Visible = false;
          }
          private void btnLimpiarS_Click(object sender, EventArgs e)
          {
               limpiar();
          }

          private void btnEliminarS_Click(object sender, EventArgs e)
          {
               if (dGWSocios.RowCount > 0)
               {
                    if (MessageBox.Show("¿Desea Eliminar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                         string c = dGWSocios.CurrentRow.Cells[0].Value.ToString();
                         string qr = "delete from memSocio where idMemS= '" + c + "' ";
                         if (dts.eliminar(qr) == true)
                         {
                              string qr2 = "delete from socio where idSocio = '" + c + "' ";
                              if (dts.eliminar(qr2) == true)
                              {
                                   MessageBox.Show("Socio Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   consultaSocio();
                                   limpiar();
                              }
                         }
                    }
               }

          }

          private void btnGuardarS_Click(object sender, EventArgs e)
          {
               if (MessageBox.Show("¿Desea Guardar?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
               {
                    string fn = dtNacimiento.Value.ToString("yyyy/MM/dd");
                    string qry = "update socio set nombre='" + txtNomS.Text.Trim() + "', fNacimiento='" + fn + "', sexo='" + txtS.Text.Trim() + "', dir ='" + txtdir.Text.Trim() + "',tel = '" + txtTel.Text.Trim() + "', correo='" + txtFB.Text.Trim() + "' where idSocio='" + txtClaveS.Text.Trim() + "' ";
                    if (dts.update(qry) == true)
                    {
                         /*qry = "update memSocio set ";
                         if (dts.update(qry)==true)
                         {*/
                         btnEliminarS.Enabled = true;
                         MessageBox.Show("Socio Actualizado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         consultaSocio();
                         limpiar();
                         /*}*/

                    }
               }
          }
          vAgregaMemS vm = new vAgregaMemS();
          string Valor1 = "", valid = "";
          private void btnAgregaMemS_Click(object sender, EventArgs e)
          {
               if (dGWHistorial.RowCount > 0)
               {
                    //con esta funcion busco dentro de todo el dtagridview
                    foreach (DataGridViewRow Row in dGWHistorial.Rows)
                    {
                         String strFila = Row.Index.ToString();
                         Valor1 = Convert.ToString(Row.Cells["Estatus"].Value);
                         valid = Convert.ToString(Row.Cells["ID"].Value);

                         if (Valor1 == "Vigente")
                         {

                              MessageBox.Show("Aun no puede agregar otro plan a este socio\n \n Tiene un pla vigente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                              break;
                         }
                         else if (Valor1 == "Expirado" || Valor1 == "Archivado")
                         {
                              vm.txtclave.Text = txtClaveS.Text;
                              vm.txtnoms.Text = txtNomS.Text;
                              vm.idUPD = valid;
                              vm.ShowDialog();
                              break;
                         }
                    }

               }
               else if (dGWHistorial.RowCount == 0)
               {
                    vm.txtclave.Text = txtClaveS.Text;
                    vm.txtnoms.Text = txtNomS.Text;
                    vm.ShowDialog();
               }

          }
          string qActualiza = "";
          string qAc = "", f1;
          private void timer1_Tick(object sender, EventArgs e)
          {
               //si esta vacio el datagridview que no salga error en la consulta
               if (dGWHistorial.RowCount > 0)
               {
                    /*compruebo Expiraciones*/
                    DataSet dataC1 = new DataSet();
                    string qqAc = "select fechatermino as C1, idControl as Id,estado as E from memSocio where idMemS = '" + txtClaveS.Text + "'";
                    dataC1 = dts.consulta(qqAc);
                    DataTable dt2 = dataC1.Tables[0];
                    string compara = "", idAc = "", es = "";
                    foreach (DataRow row in dt2.Rows)
                    {
                         compara = row["C1"].ToString();
                         idAc = row["Id"].ToString();
                         es = row["E"].ToString();

                    }
                    DateTime fechaF = Convert.ToDateTime(compara).Date;
                    DateTime FechAc = DateTime.Now.Date;
                    if (fechaF <= FechAc) // Si la fecha indicada es menor o igual a la fecha actual
                    {
                         if (es == "Vigente")
                         {
                              //Operación Expirada
                              timer1.Stop();
                              qActualiza = "update memSocio set estado='Expirado' where idControl = '" + idAc + "' and idMemS = '" + txtClaveS.Text + "'";
                              if (dts.update(qActualiza) == true)
                              {
                                   MessageBox.Show("Este Socio tiene un plan Expirado!!", "Expirado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                   consultaHistorial(txtClaveS.Text);
                                   timer1.Stop();
                              }
                         }
                         else if (es == "Archivado")
                         {

                         }

                    }
                    else
                    {

                         //Operación Aun Vigente
                         timer1.Stop();
                         //MessageBox.Show("Aun Vigente");

                    }
               }
               else
               {

                    timer1.Stop();
                    MessageBox.Show("Socio sin plan de membresia.\n Necesita agregar plan", "Sin plan", MessageBoxButtons.OK, MessageBoxIcon.Stop);
               }

          }
          string q12 = "";
          private void btnEliminaMemS_Click(object sender, EventArgs e)
          {
               if (dGWHistorial.RowCount > 0)
               {
                    if (dGWHistorial.CurrentRow.Cells[7].Value.ToString() == "Vigente")
                    {
                         if (MessageBox.Show("La membresia esta vigente \n ¿Quiere eliminarla? \n Se Limpiara todo el Historial", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                         {
                              q12 = "delete from memSocio where idMemS='" + dGWHistorial.CurrentRow.Cells[1].Value.ToString() + "' ";
                              if (dts.eliminar(q12) == true)
                              {
                                   MessageBox.Show("Historial Actualizado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   consultaHistorial(txtClaveS.Text);
                              }
                         }
                    }
                    else if (dGWHistorial.CurrentRow.Cells[7].Value.ToString() == "Expirado" || dGWHistorial.CurrentRow.Cells[7].Value.ToString() == "Archivado")
                    {
                         q12 = "delete from memSocio where idControl='" + dGWHistorial.CurrentRow.Cells[0].Value.ToString() + "' ";
                         if (dts.eliminar(q12) == true)
                         {
                              MessageBox.Show("Historial Actualizado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              consultaHistorial(txtClaveS.Text);
                         }
                    }
               }
               else
               {
                    MessageBox.Show("No Cuenta con plan de Membresias", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
               }
          }

          #region condicionlaDTGW
          private void dGWHistorial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
          {
               if (this.dGWHistorial.Columns[e.ColumnIndex].Name == "Estatus")
               {
                    if (e.Value.ToString() == "Vigente")
                    {
                         e.CellStyle.ForeColor = Color.Green;
                         e.CellStyle.BackColor = Color.LightGreen;

                    }
                    else if (e.Value.ToString() == "Expirado")
                    {
                         e.CellStyle.ForeColor = Color.Red;
                         e.CellStyle.BackColor = Color.Salmon;
                    }
                    if (e.Value.ToString() == "Archivado")
                    {
                         e.CellStyle.ForeColor = Color.Blue;
                         e.CellStyle.BackColor = Color.LightBlue;

                    }
               }
          }


          #endregion

          #region validacionesTXT
          restricciones val = new restricciones();

          private void txtD_KeyPress(object sender, KeyPressEventArgs e)
          {
               val.solonumerosEnteros(e);
          }

          private void txtM_KeyPress(object sender, KeyPressEventArgs e)
          {
               val.solonumerosEnteros(e);
          }

          private void txtAñoF_KeyPress(object sender, KeyPressEventArgs e)
          {
               val.solonumerosEnteros(e);
          }

          private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
          {
               val.solonumerosEnteros(e);
          }

          private void txtS_KeyPress(object sender, KeyPressEventArgs e)
          {
               val.sololetras(e);

          }

          private void txtS_KeyDown(object sender, KeyEventArgs e)
          {

          }

          private void txtS_TextChanged(object sender, EventArgs e)
          {

          }

          private void textBox1_KeyUp(object sender, KeyEventArgs e)
          {
               string salida = "";
               string[] pBusqueda = this.textBox1.Text.Split(' ');
               foreach (string p in pBusqueda)
               {
                    if (salida.Length == 0)
                    {
                         salida = "(Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%' OR Clave LIKE '%" + p + "%' OR Clave LIKE '" + p + "%' )";
                    }
                    else
                    {
                         salida += "AND (Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%' OR Clave LIKE '%" + p + "%' OR Clave LIKE '" + p + "%')";
                    }
               }
               this.miFiltro.RowFilter = salida;

          }

          private void btn_Imprimir_Click(object sender, EventArgs e)
          {
               if (dGWSocios.RowCount > 0)
               {
                    string fileName = txtClaveS.Text + ".pdf";
                    string filePath = Path.Combine(Application.StartupPath + "\\ARCHIVOS", fileName);
                    if (File.Exists(filePath))
                    {
                         Process.Start(filePath);
                    }
                    else
                    {
                         CreaPDF(filePath);
                         Process.Start(filePath);
                         btn_enviar_correo.Visible = true;
                    }
                    
               }


          }

          private void CreaPDF(string filePath)
          {
               //RUTA DE LOS REPORTES
               string rptsPath = Path.Combine(Application.StartupPath + "\\RPTS");
               StringBuilder query = new StringBuilder();
               query.AppendFormat("select * from socio ");
               query.AppendFormat("where idsocio = '{0}'", txtClaveS.Text);

               DataTable dtSocio = dts.SelectDatatable(query.ToString());

               Framework.CrystalReports.Report reportCR = new Framework.CrystalReports.Report(rptsPath, "rptCredencial.rpt");
               reportCR.AddTable("socio", dtSocio);
               reportCR.FormatType = FormatType.PDF;
               buffer = reportCR.Start();
               File.WriteAllBytes(filePath, buffer);
               Process.Start(filePath);

          }

          private void btn_enviar_correo_Click(object sender, EventArgs e)
          {
               string fileName = txtClaveS.Text + ".pdf";
               string filePath = Path.Combine(Application.StartupPath + "\\ARCHIVOS", fileName);

               string query = "select idsocio from Socio where idsocio = '" + txtClaveS.Text + "'";
               string ClaveUsuario = dts.ejecutaEscalar(query);

               string query2 = "select correo from Socio where idsocio = '" + txtClaveS.Text + "'";
               string CorreoUsuario = dts.ejecutaEscalar(query2);

               if (CorreoUsuario == "")
               {
                    MessageBox.Show("Socio no tiene un correo registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
               }

               string asunto = "";
               string nombre = "";

               if (dGWSocios.SelectedRows.Count == 0)
               {
                    MessageBox.Show("Selecciona un socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
               }
               else
               {
                    asunto = " ¡Bienvenido/a al gimnasio!";
                    nombre = txtNomS.Text;

                    Email correo = new Email();
                    if (correo.Enviar(CorreoUsuario,asunto,nombre,filePath))
                    {

                    }
               }
          }

          private void bnt_rest_Click(object sender, EventArgs e)
          {
               this.WindowState = FormWindowState.Maximized;
          }

          private void btn_min_Click(object sender, EventArgs e)
          {
               this.WindowState = FormWindowState.Minimized;
          }

          private void panel1_Paint(object sender, PaintEventArgs e)
          {

          }

          private void dGWHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {

          }

          private void btnActualizar_Click(object sender, EventArgs e)
          {
               consultaHistorial(txtClaveS.Text);
          }

          private void txtNomS_KeyPress(object sender, KeyPressEventArgs e)
          {
               val.sololetras(e);
          }
          #endregion
     }
}
