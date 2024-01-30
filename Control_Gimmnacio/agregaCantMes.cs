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
        public agregaCantMes()
        {
            InitializeComponent();
            numMes.Value = 1;
            cmbSocio.Visible = false;
            checkBoxSocio.Checked = false;
            txtTotal.Text = "30.00";
            txtTotal.TextAlign = HorizontalAlignment.Left;
          }
        conexionDatos dts = new conexionDatos();
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (numMes.Value!=0)
            {
                    StringBuilder query = new StringBuilder();
                    query.Append("insert into visitante (cantidad,fecha,precio,");
                    if (string.IsNullOrEmpty(txtNombre.Text))
                    {
                         query.Append("Socio) ");
                         query.AppendFormat("VALUES({0},{1},{2},'{3}')", numMes.Value, "CONVERT(date, SYSDATETIME())", txtTotal.Text, cmbSocio.Text);
                    }
                    else
                    {
                         query.Append("Visitante) ");
                         query.AppendFormat("VALUES({0},{1},{2},'{3}')", numMes.Value, "CONVERT(date, SYSDATETIME())", txtTotal.Text, txtNombre.Text);
                    }
                    
                                             
                    if (dts.insertar(query.ToString())==true)
                    {
                         AgregaHistorialVisitas();
                         MessageBox.Show("Visita Agregada","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);
                         this.Close();
                    }
                
            }
            else
            {
                MessageBox.Show("Agrega por lo menos un Visitante","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

          private void AgregaHistorialVisitas()
          {              
                    StringBuilder query = new StringBuilder();
                    query.Append("insert into visitante_historial (cantidad,fecha,precio,");
                    if (string.IsNullOrEmpty(txtNombre.Text))
                    {
                         query.Append("Socio) ");
                         query.AppendFormat("VALUES({0},{1},{2},'{3}')", numMes.Value, "CONVERT(date, SYSDATETIME())", txtTotal.Text, cmbSocio.Text);
                    }
                    else
                    {
                         query.Append("Visitante) ");
                         query.AppendFormat("VALUES({0},{1},{2},'{3}')", numMes.Value, "CONVERT(date, SYSDATETIME())", txtTotal.Text, txtNombre.Text);
                    }
                     dts.insertar(query.ToString());
          }

          private void label1_Click(object sender, EventArgs e)
        {

        }

        private void agregaCantMes_Load(object sender, EventArgs e)
        {
            numMes.Value = 1;

        }

          private void LlenarComboBoxSocios(string nombre)
          {
               StringBuilder q = new StringBuilder();

               // Agregar texto al StringBuilder
               q.Append("select nombre,idsocio from socio ");
               q.Append("INNER JOIN memSocio ON ");
               q.Append("socio.idsocio = memSocio.idmems ");
               q.Append("where memsocio.estado = 'Vigente'");
               if (nombre != "")
               {
                    q.AppendFormat("and {0}",nombre);
               }
               cmbSocio.DataSource = dts.consulta(q.ToString()).Tables[0];
               cmbSocio.DisplayMember = "Nombre";
               cmbSocio.ValueMember = "idSocio";
          }

          private void checkBoxSocio_CheckedChanged_1(object sender, EventArgs e)
          {
               if (checkBoxSocio.Checked)
               {

                    cmbSocio.Visible = true;
                    txtNombre.Visible = false;
                    LlenarComboBoxSocios("");
               }
               else
               {

                    cmbSocio.Visible = false;
                    txtNombre.Visible = true;
                    // Limpiar el ComboBox si es necesario
                    if (cmbSocio.DataSource == null)
                    {
                         cmbSocio.Items.Clear();
                    }
                    else
                    {
                         // Si tiene un origen de datos, asigna null al origen de datos y luego limpia los elementos
                         cmbSocio.DataSource = null;
                         cmbSocio.Items.Clear();
                    }
               }
          }

          private void cmbSocio_KeyUp(object sender, KeyEventArgs e)
          {
               string salida = "";
               string[] pBusqueda = this.cmbSocio.Text.Split(' ');
               foreach (string p in pBusqueda)
               {
                    if (salida.Length == 0)
                    {
                         salida = "(Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%')";
                    }
                    else
                    {
                         salida += "AND (Nombre LIKE '% " + p + "%' OR Nombre LIKE '" + p + "%')";
                    }
               }
               LlenarComboBoxSocios(salida);
               txtTotal.Text = "00.00";
          }

          private void txtTotal_TextChanged(object sender, EventArgs e)
          {

               //if (decimal.TryParse(txtTotal.Text, out decimal total) && total >= 0)
               //{
               //     txtTotal.Text = total.ToString("N2");
               //}
               //else
               //{

               //}
               txtTotal.TextAlign = HorizontalAlignment.Left;
               
          }

          private void txtTotal_Click(object sender, EventArgs e)
          {
              
          }
     }
}
