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
    public partial class FrmVenta : Form
    {
        bool esNuevo = false;
        public FrmVenta()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var ventas = VentaCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = ventas;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["fecha"].HeaderText = "Fecha";
            dgvLista.Columns["idCliente"].HeaderText = "Id Cliente";
            dgvLista.Columns["idUsuario"].HeaderText = "Id Usuario";

            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
           // btnEditar.Enabled = ventas.Count > 0;
            btnEliminar.Enabled = ventas.Count > 0;
            if (ventas.Count > 0) dgvLista.Rows[0].Cells["fecha"].Selected = true;
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            Size = new Size(830, 348);
            listar();
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(830, 489);
            limpiar();
            txtFecha.Focus();
        }


        private void dtpFecha_ValueChanged_1(object sender, EventArgs e)
        {
            DateTime fecha = dtpFecha.Value;
            txtFecha.Text = fecha.ToShortDateString();
        }


        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(830, 462);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var venta = VentaCln.get(id);
            txtFecha.Text = Convert.ToString(venta.fecha);
            txtIdCliente.Text = Convert.ToString(venta.idCliente);
            txtIdUsuario.Text = Convert.ToString(venta.idUsuario);
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
            erpFecha.SetError(txtFecha, "");
            erpIdCliente.SetError(txtIdCliente, "");

            if (string.IsNullOrEmpty(txtIdCliente.Text))
            {
                esValido = false;
                erpIdCliente.SetError(txtIdCliente, "El campo IdVenta es obligatorio");
            }
            

            return esValido;
        }


        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (validar())
            {
                var venta = new Venta();
                venta.idCliente = Convert.ToInt32(txtIdCliente.Text);
                venta.fecha = Convert.ToDateTime(txtFecha.Text);
                venta.idUsuario = Convert.ToInt32(txtIdUsuario.Text);


                venta.usuarioRegistro = Util.usuario.usuario1;

                if (esNuevo)
                {
                    venta.fechaRegistro = DateTime.Now;
                    venta.estado = 1;
                    VentaCln.insertar(venta);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    venta.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    VentaCln.actualizar(venta);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Venta guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void limpiar()
        {
            txtFecha.Text = "dd/mm/aaaa";
            txtIdCliente.Text = string.Empty;
            txtIdUsuario.Text = string.Empty;
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string fecha = dgvLista.Rows[index].Cells["fecha"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja la venta {fecha}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                VentaCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("Venta eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            listar();
        }


        private void btnCerrar_Click_2(object sender, EventArgs e)
        {
            Close();
        }

        private void txtIdUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
