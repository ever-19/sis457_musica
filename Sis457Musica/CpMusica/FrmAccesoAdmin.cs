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
    public partial class FrmAccesoAdmin : Form
    {
        


        bool esNuevo = false;
        public FrmAccesoAdmin()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var accesoadmins = AccesoAdminCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = accesoadmins;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["codigo"].HeaderText = "Código";
            btnEditar.Enabled = accesoadmins.Count > 0;
            btnEliminar.Enabled = accesoadmins.Count > 0;
            if (accesoadmins.Count > 0) dgvLista.Rows[0].Cells["codigo"].Selected = true;
        }

        private void FrmAccesoAdmin_Load(object sender, EventArgs e)
        {
            Size = new Size(830, 348);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(830, 462);
            txtCodigo.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(830, 462);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var accesoadmin = AccesoAdminCln.get(id);
            txtCodigo.Text = accesoadmin.codigo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(830, 347);
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
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
            erpCodigo.SetError(txtCodigo, "");

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                esValido = false;
                erpCodigo.SetError(txtCodigo, "El campo Código es obligatorio");
            }
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var accesoadmin = new AccesoAdmin();
                accesoadmin.codigo = txtCodigo.Text.Trim();
                accesoadmin.usuarioRegistro = "SIS457 - Musica";

                if (esNuevo)
                {
                    accesoadmin.fechaRegistro = DateTime.Now;
                    accesoadmin.estado = 1;
                    AccesoAdminCln.insertar(accesoadmin);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    accesoadmin.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    AccesoAdminCln.actualizar(accesoadmin);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Codigo de Acceso guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void limpiar()
        {
            txtCodigo.Text = string.Empty;
        }

       

       

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string codigo = dgvLista.Rows[index].Cells["codigo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el Codigo de acceso {codigo}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                AccesoAdminCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("Codigo de Acceso eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        
    }
}
