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
    public partial class FrmArticulo : Form
    {

        FrmAutenticacion frmAutenticacion;
        public FrmArticulo(FrmAutenticacion frmAutenticacion)
        {
            InitializeComponent();
            this.frmAutenticacion = frmAutenticacion;
        }
        private void FrmArticulo_FormClosing(object sender, FormClosingEventArgs e)
        {
           // frmAutenticacion.Visible = true;
        }
       






        bool esNuevo = false;
        public FrmArticulo()
        {
            InitializeComponent();
            cargarCategoria();
        }

        private void listar()
        {
            var articulos = ArticuloCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = articulos;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["codigo"].HeaderText = "Código";
            dgvLista.Columns["descripcion"].HeaderText = "Descripción";

            dgvLista.Columns["nombre_categoria"].HeaderText = "Categoria"; 
            dgvLista.Columns["unidadMedida"].HeaderText = "Unidad de Medida";
            
            dgvLista.Columns["precio"].HeaderText = "Precio";
            dgvLista.Columns["cantidadExistente"].HeaderText = "Cantidad Existente";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
            btnEditar.Enabled = articulos.Count > 0;
            btnEliminar.Enabled = articulos.Count > 0;
            if (articulos.Count > 0) dgvLista.Rows[0].Cells["descripcion"].Selected = true;
        }

        private void cargarCategoria()
        {
            cbxCategoria.DataSource = CategoriaCln.listar();
            cbxCategoria.DisplayMember = "nombre";
            cbxCategoria.ValueMember = "id";

        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            Size = new Size(830, 348);
            listar();
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(830, 489);
            txtCodigo.Focus();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(830, 462);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var articulo = ArticuloCln.get(id);
            txtCodigo.Text = articulo.codigo;
            txtDescripcion.Text = articulo.descripcion;
            txtMarca.Text = articulo.marca;
            cbxUnidadMedida.Text = articulo.unidadMedida;
            cbxCategoria.Text = Convert.ToString(articulo.idCategoria);
            nudPrecio.Value = articulo.precio;
            nudCantidadExistente.Value = articulo.cantidadExistente;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Size = new Size(830, 347);
            limpiar();
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
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
            erpDescripcion.SetError(txtDescripcion, "");
            erpUnidadMedida.SetError(cbxUnidadMedida, "");
            erpCategoria.SetError(cbxCategoria, "");
            erpPrecio.SetError(nudPrecio, "");
            erpCantidad.SetError(nudCantidadExistente, "");

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                esValido = false;
                erpCodigo.SetError(txtCodigo, "El campo Código es obligatorio");
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                esValido = false;
                erpDescripcion.SetError(txtDescripcion, "El campo Descripción es obligatorio");
            }
            if (string.IsNullOrEmpty(cbxUnidadMedida.Text))
            {
                esValido = false;
                erpUnidadMedida.SetError(cbxUnidadMedida, "El campo Unidad de Medida es obligatorio");
            }

            if (string.IsNullOrEmpty(cbxCategoria.Text))
            {
                esValido = false;
                erpCategoria.SetError(cbxCategoria, "El campo Categoria es obligatorio");
            }

            if (string.IsNullOrEmpty(nudPrecio.Text))
            {
                esValido = false;
                erpPrecio.SetError(nudPrecio, "El campo Precio de Venta es obligatorio");
            }
            if (string.IsNullOrEmpty(nudCantidadExistente.Text))
            {
                esValido = false;
                erpCantidad.SetError(nudCantidadExistente, "El campo Cantidad es obligatorio");
            }
            if (nudCantidadExistente.Value < 0)
            {
                esValido = false;
                erpCantidad.SetError(nudCantidadExistente, "El campo Cantidad debe ser mayor o igual a 0");
            }
            return esValido;
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (validar())
            {
                var articulo = new Articulo();
                articulo.codigo = txtCodigo.Text.Trim();
                articulo.descripcion = txtDescripcion.Text.Trim();
                articulo.unidadMedida = cbxUnidadMedida.Text;
                articulo.marca = txtMarca.Text;
                articulo.idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                articulo.precio = nudPrecio.Value;
                articulo.cantidadExistente = Convert.ToInt32(nudCantidadExistente.Value);
                articulo.usuarioRegistro = "SIS457 - Musica";

                if (esNuevo)
                {
                    articulo.fechaRegistro = DateTime.Now;
                    articulo.estado = 1;
                    articulo.idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                    ArticuloCln.insertar(articulo);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    articulo.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    ArticuloCln.actualizar(articulo);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Articulo guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       

        private void limpiar()
        {
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cbxUnidadMedida.SelectedIndex = -1;
            cbxCategoria.SelectedIndex = -1;
            
            nudPrecio.Value = 0;
            nudCantidadExistente.Value = 0;
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string codigo = dgvLista.Rows[index].Cells["codigo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el articulo {codigo}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                ArticuloCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("Articulo eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
    }
}
