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
    public partial class FrmUsuario : Form
    {
       

        

        bool esNuevo = false;
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var usuarios = UsuarioCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = usuarios;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["usuario"].HeaderText = "Usuario";
            dgvLista.Columns["clave"].HeaderText = "Clave";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
            btnEditar.Enabled = usuarios.Count > 0;
            btnEliminar.Enabled = usuarios.Count > 0;
            if (usuarios.Count > 0) dgvLista.Rows[0].Cells["usuario"].Selected = true;
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            Size = new Size(816, 334);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(816, 479);
            txtUsuario.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {  
            esNuevo = false;
            Size = new Size(816, 479);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var usuario = UsuarioCln.get(id);
            txtIdEmpleado.Text = Convert.ToString(usuario.idEmpleado);
            txtUsuario.Text = usuario.usuario1;
            txtClave.Text = usuario.clave;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(816, 334);
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
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
            erpUsuario.SetError(txtUsuario, "");
            erpClave.SetError(txtClave, "");

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                esValido = false;
                erpUsuario.SetError(txtUsuario, "El campo Código es obligatorio");
            }
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                esValido = false;
                erpClave.SetError(txtClave, "El campo Descripción es obligatorio");
            }
            
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
               
                var usuario = new Usuario();
                usuario.idEmpleado =  Convert.ToInt32(txtIdEmpleado.Text);
                usuario.usuario1 = txtUsuario.Text.Trim();
                usuario.clave = Util.Encrypt(txtClave.Text);
                usuario.rol = cbxRol.Text.Trim();
                usuario.usuarioRegistro = "SIS457 - Musica";

                if (esNuevo)
                {
                    usuario.fechaRegistro = DateTime.Now;
                    usuario.estado = 1;
                    UsuarioCln.insertar(usuario);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    usuario.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    UsuarioCln.actualizar(usuario);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Usuario guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        

        private void limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string usuario1 = dgvLista.Rows[index].Cells["codigo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el usuario {usuario1}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                UsuarioCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("Usuario eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
