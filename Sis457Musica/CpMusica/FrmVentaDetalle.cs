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
    public partial class FrmVentaDetalle : Form
    {
        bool esNuevo = false;
        public FrmVentaDetalle()
        {
            InitializeComponent();
        }

     

        private void listar()
        {
            var ventadetalles = VentaDetalleCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = ventadetalles;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            //dgvLista.Columns["idCategoria"].Visible = false;
            dgvLista.Columns["descripcion_articulo"].HeaderText = "Descripcion";
            dgvLista.Columns["cantidad"].HeaderText = "Cantidad";
            dgvLista.Columns["precioUnitario"].HeaderText = "Precio Unitario";

            dgvLista.Columns["precioTotal"].HeaderText = "Precio Total";
            dgvLista.Columns["tipoPago"].HeaderText = " Tipo de pago";


            //btnEditar.Enabled = ventadetalles.Count > 0;
            btnEliminar.Enabled = ventadetalles.Count > 0;
            if (ventadetalles.Count > 0) dgvLista.Rows[0].Cells["descripcion"].Selected = true;
        }

        private void cargarArticulos()
        {
            cbxDescripcion.DataSource = ArticuloCln.listar();
            cbxDescripcion.DisplayMember = "descripcion";
            cbxDescripcion.ValueMember = "id";

        }

        private void FrmVentaDetalle_Load(object sender, EventArgs e)
        {
            Size = new Size(830, 348);
            listar();
            cargarArticulos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(830, 489);
            limpiar();
            txtCantidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(830, 462);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var ventadetalle = VentaDetalleCln.get(id);
            txtCantidad.Text = Convert.ToString(ventadetalle.cantidad);
            txtPrecioUnitario.Text = Convert.ToString(ventadetalle.precioUnitario);
            txtPrecioTotal.Text = Convert.ToString(ventadetalle.precioTotal);
            cbxTipoPago.Text = ventadetalle.tipoPago;
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
           erpCantidad.SetError(txtCantidad, "");
           erpPrecioUnitario.SetError(txtPrecioUnitario, "");
           erpPrecioTotal.SetError(txtPrecioTotal, "");
           erpDescripcion.SetError(cbxDescripcion, "");
           erpTipoPago.SetError(cbxTipoPago, "");
            erpIdVenta.SetError(txtIdVenta, "");
            erpIdArticulo.SetError(txtIdArticulo, "");


            if (string.IsNullOrEmpty(txtCantidad.Text))
           {
               esValido = false;
                erpCantidad.SetError(txtCantidad, "El campo Cantidad es obligatorio");
           }
           if (string.IsNullOrEmpty(cbxDescripcion.Text))
           {
               esValido = false;
               erpDescripcion.SetError(cbxDescripcion, "El campo Descripción es obligatorio");
           }
           if (string.IsNullOrEmpty(txtPrecioUnitario.Text))
           {
               esValido = false;
                erpPrecioUnitario.SetError(txtPrecioUnitario, "El campo Precio unitario de Medida es obligatorio");
           }

           if (string.IsNullOrEmpty(txtPrecioTotal.Text))
           {
               esValido = false;
                erpPrecioTotal.SetError(txtPrecioTotal, "El campo precio total es obligatorio");
           }

            if (string.IsNullOrEmpty(cbxTipoPago.Text))
            {
                esValido = false;
                erpTipoPago.SetError(cbxTipoPago, "El campo precio total es obligatorio");
            }
            if (string.IsNullOrEmpty(txtIdVenta.Text))
            {
                esValido = false;
                erpIdVenta.SetError(txtIdVenta, "El campo id venta es obligatorio");
            }
            if (string.IsNullOrEmpty(cbxTipoPago.Text))
            {
                esValido = false;
                erpIdArticulo.SetError(txtIdVenta, "El campo id articulo es obligatorio");
            }


            return esValido;
       }


        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (validar())
            {
                var ventadetalle = new VentaDetalle();
                ventadetalle.cantidad = Convert.ToInt32(txtCantidad.Text);
                ventadetalle.precioUnitario = Convert.ToInt32(txtPrecioUnitario.Text);
                ventadetalle.precioTotal = Convert.ToInt32(txtPrecioTotal.Text);
                ventadetalle.tipoPago = cbxTipoPago.Text;
                ventadetalle.idVenta = Convert.ToInt32(txtIdVenta.Text);
                ventadetalle.idArticulo = Convert.ToInt32(txtIdArticulo.Text);

                ventadetalle.usuarioRegistro = Util.usuario.usuario1;

                if (esNuevo)
                {
                    ventadetalle.fechaRegistro = DateTime.Now;
                    ventadetalle.estado = 1;
                    ventadetalle.idVenta = Convert.ToInt32(txtIdVenta.Text);
                    ventadetalle.idArticulo = Convert.ToInt32(txtIdArticulo.Text);
        
                    ventadetalle.idVenta = Convert.ToInt32(txtIdVenta.Text);
                    ventadetalle.idArticulo = Convert.ToInt32(txtIdArticulo.Text);
                    VentaDetalleCln.insertar(ventadetalle);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    ventadetalle.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    VentaDetalleCln.actualizar(ventadetalle);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("VentaDetalle guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


       private void limpiar()
       {
           txtCantidad.Text = string.Empty;
           txtPrecioUnitario.Text = string.Empty;
           txtPrecioTotal.Text = string.Empty;

       }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string codigo = dgvLista.Rows[index].Cells["codigo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el ventadetalle {codigo}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                VentaDetalleCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("VentaDetalle eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


       private void btnBuscar_Click_1(object sender, EventArgs e)
       {
           listar();
       }


    }
}
