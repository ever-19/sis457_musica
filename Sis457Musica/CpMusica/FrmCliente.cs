using CadMusica;
using ClnMusica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpMusica
{
    public partial class FrmCliente : Form
    {
        bool esNuevo = false;
        public FrmCliente()
        {
            InitializeComponent();
        }

        
   

        private void listar()
        {
            var clientes = ClienteCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = clientes;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            //dgvLista.Columns["idCategoria"].Visible = false;
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cedula Identidad";
            dgvLista.Columns["nombre"].HeaderText = "Nombre";
            dgvLista.Columns["primerApellido"].HeaderText = "Primer Apellido";
            dgvLista.Columns["segundoApellido"].HeaderText = "Segundo Apellido";
            dgvLista.Columns["direccion"].HeaderText = "Direccion";

            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
            btnEditar.Enabled = clientes.Count > 0;
            btnEliminar.Enabled = clientes.Count > 0;
            if (clientes.Count > 0) dgvLista.Rows[0].Cells["cedulaIdentidad"].Selected = true;
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Size = new Size(830, 348);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(830, 489);
            limpiar();
            txtCedulaIdentidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(830, 462);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var cliente = ClienteCln.get(id);
            txtCedulaIdentidad.Text = cliente.cedulaIdentidad;
            txtNombre.Text = cliente.nombre;
            txtPrimerApellido.Text = cliente.primerApellido;
            txtSegundoApellido.Text = cliente.segundoApellido;
            txtDireccion.Text = cliente.direccion;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(830, 347);
            limpiar();
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
        }


        private void btnBuscar_Click_2(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private bool validar()
        {
            bool esValido = true;
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpNombre.SetError(txtNombre, "");
            erpPrimerApellido.SetError(txtPrimerApellido, "");
            erpSegundoApellido.SetError(txtSegundoApellido, "");
            erpDireccion.SetError(txtDireccion, "");

            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                esValido = false;
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo cedula identidad es obligatorio");
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                esValido = false;
                erpNombre.SetError(txtNombre, "El campo nombre es obligatorio");
            }
            if (string.IsNullOrEmpty(txtPrimerApellido.Text))
            {
                esValido = false;
                erpPrimerApellido.SetError(txtPrimerApellido, "El campo primer apellido de Medida es obligatorio");
            }

            if (string.IsNullOrEmpty(txtSegundoApellido.Text))
            {
                esValido = false;
                erpSegundoApellido.SetError(txtSegundoApellido, "El campo segundo apellido es obligatorio");
            }

            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                esValido = false;
                erpDireccion.SetError(txtDireccion, "El campo Direccion es obligatorio");
            }
            
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cliente = new Cliente();
                cliente.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                cliente.nombre = txtNombre.Text.Trim();
                cliente.primerApellido = txtPrimerApellido.Text.Trim(); ;
                cliente.segundoApellido = txtSegundoApellido.Text.Trim(); ;
                cliente.direccion = txtDireccion.Text.Trim(); ;
                cliente.usuarioRegistro = "ever";//Util.usuario.usuario1;

                if (esNuevo)
                {
                    cliente.fechaRegistro = DateTime.Now;
                    cliente.estado = 1;
                    ClienteCln.insertar(cliente);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    cliente.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    ClienteCln.actualizar(cliente);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Cliente guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void limpiar()
        {
            txtCedulaIdentidad.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string codigo = dgvLista.Rows[index].Cells["codigo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el cliente {codigo}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                ClienteCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("Cliente eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            listar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
