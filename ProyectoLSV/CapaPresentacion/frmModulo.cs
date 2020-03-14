using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class frmModulo : Form
    {

      
        private bool isEditar = false;
        public frmModulo()
        {

            InitializeComponent();
            this.ttmensaje.SetToolTip(this.txtNombre, "Falta Nombre del Modulo");
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "LSV", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "LSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtID.Text = string.Empty;
        }

        
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.txtID.ReadOnly = !valor;
        }

        private void Botones()
        {
            this.Habilitar(false);
            this.btnGuardar.Enabled = false;
            this.btnEditar.Enabled = true;
            this.btnCancelar.Enabled = false;

        }

       
        private void OcultarColumnas()
        {
            this.dgvModulo.Columns[0].Visible = false;
        }

        private void labels()
        {
            lblTotalModulos.Text = dgvModulo.Rows[0].Cells[1].Value.ToString(); //OMGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG
          
        }

        private void Mostrar()
        {

            this.dgvModulo.DataSource = LModulo.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Modulos:" + Convert.ToString(dgvModulo.Rows.Count);
        }

        private void Buscar()
        {
            this.dgvModulo.DataSource = LModulo.Buscar(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Modulos: " + Convert.ToString(dgvModulo.Rows.Count);
        }

        private void frmModulo_Load(object sender, EventArgs e)
        {
            dgvModulo.Visible = false; //esconder la tabla 
            

            this.Top = 0;
            this.Left = 0;

            
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
            this.labels();


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.isEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.txtID.Enabled = false;

            if (!this.txtID.Text.Equals(""))
            {
                this.isEditar = true;
                this.btnGuardar.Enabled = true;
                this.btnCancelar.Enabled = true;
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe de seleccionar primero el registro a Modificar");
            }
        }


       

        private void dgvModulo_DoubleClick(object sender, EventArgs e)
        {
            this.txtID.Text = Convert.ToString(this.dgvModulo.CurrentRow.Cells["idmodulo"].Value);
            this.txtNombre.Text = Convert.ToString(this.dgvModulo.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dgvModulo.CurrentRow.Cells["descripcion"].Value);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, seran remarcados");
                    erroricono.SetError(txtNombre, "Ingrese Nombre");
                }
                else
                {
                    rpta = LModulo.Editar(Convert.ToInt32(this.txtID.Text), this.txtNombre.Text,
                    this.txtDescripcion.Text);
                }

                if (rpta.Equals("OK"))
                {
                    if (this.isEditar)
                    {
                        this.MensajeOk("Se Actualizó de forma correcta el registro");
                    }
                }
                else
                {
                    this.MensajeError(rpta);
                }


                this.isEditar = false;
                this.Botones();
                this.Limpiar();
                this.Mostrar();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
