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
    public partial class FrmCategoria : Form
    {


        bool esNuevo = false;
        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var categorias = CategoriaCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = categorias;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;

            dgvLista.Columns["nombre"].HeaderText = "Nombre";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
            btnEditar.Enabled = categorias.Count > 0;
            btnEliminar.Enabled = categorias.Count > 0;
            if (categorias.Count > 0) dgvLista.Rows[0].Cells["nombre"].Selected = true;
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            Size = new Size(830, 348);
            listar();
        }



        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(830, 489);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var categoria = CategoriaCln.get(id);
            txtNombre.Text = categoria.nombre;
        }


        private void limpiar()
        {
            txtNombre.Text = string.Empty;
        }

        private bool validar()
        {
            bool esValido = true;
            erpNombre.SetError(txtNombre, "");

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                esValido = false;
                erpNombre.SetError(txtNombre, "El campo nombre de categoria es obligatorio");
            }
            return esValido;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {



            if (validar())
            {
                var categoria = new Categoria();
            categoria.nombre = txtNombre.Text.Trim();

            categoria.usuarioRegistro = Util.usuario.usuario1;

                if (esNuevo)
                {
                    categoria.fechaRegistro = DateTime.Now;
                    categoria.estado = 1;
                    CategoriaCln.insertar(categoria);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    categoria.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    CategoriaCln.actualizar(categoria);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Categoria guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(830, 489);
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string nombre = dgvLista.Rows[index].Cells["nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja la categoria {nombre}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                CategoriaCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("Categoria eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(830, 347);
            limpiar();
        }
    }
}
