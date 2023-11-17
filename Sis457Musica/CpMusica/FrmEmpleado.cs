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
    public partial class FrmEmpleado : Form
    {

        bool esNuevo = false;
        public FrmEmpleado()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var empleados = EmpleadoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = empleados;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cedula Identidad";
            dgvLista.Columns["nombre"].HeaderText = "nombre";
            dgvLista.Columns["apellidoPaterno"].HeaderText = "apellidoPaterno";
            dgvLista.Columns["apellidoMaterno"].HeaderText = "apellidoMaterno";
            dgvLista.Columns["sexo"].HeaderText = "sexo";
            dgvLista.Columns["fechaContrato"].HeaderText = "fechaContrato";
            dgvLista.Columns["cargo"].HeaderText = "cargo";
            dgvLista.Columns["celular"].HeaderText = "celular";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
            btnEditar.Enabled = empleados.Count > 0;
            btnEliminar.Enabled = empleados.Count > 0;
            if (empleados.Count > 0) dgvLista.Rows[0].Cells["cedulaIdentidad"].Selected = true;
        }
        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            Size = new Size(848, 348);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(848, 587);
            txtCedulaIdentidad.Focus();
        }
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            DateTime fecha = dtpFecha.Value;
            txtFecha.Text = fecha.ToShortDateString();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(848, 587);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var empleado = EmpleadoCln.get(id);
            txtCedulaIdentidad.Text = empleado.cedulaIdentidad;
            txtNombre.Text = empleado.nombre;
            txtPrimerApellido.Text = empleado.primerApellido;
            txtSegundoApellido.Text = empleado.segundoApellido;
            cbxSexo.Text = empleado.sexo;
            txtFecha.Text = Convert.ToString(empleado.fechaContrato);
            txtCelular.Text = Convert.ToString(empleado.celular);
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
        

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }



        private void limpiar()
        {
            txtCedulaIdentidad.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            cbxSexo.SelectedIndex = -1;
            txtFecha.Text = string.Empty;
            cbxCargo.SelectedIndex = -1;
            txtCelular.Text = string.Empty;
        }



        private bool validar()
        {
            bool esValido = true;
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpNombre.SetError(txtNombre, "");
            erpApellidoPaterno.SetError(txtPrimerApellido, "");
            erpApellidoPaterno.SetError(txtPrimerApellido, "");
            erpApellidoMaterno.SetError(txtSegundoApellido, "");
            erpSexo.SetError(cbxSexo, "");
            erpFechaContrato.SetError(txtFecha, "");
            erpCargo.SetError(cbxCargo, "");
            erpCelular.SetError(txtCelular, "");

            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                esValido = false;
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo Cedula Identidad es obligatorio");
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                esValido = false;
                erpNombre.SetError(txtNombre, "El campo Nombre es obligatorio");
            }
            if (string.IsNullOrEmpty(txtPrimerApellido.Text))
            {
                esValido = false;
                erpApellidoPaterno.SetError(txtPrimerApellido, "El campo Apellido Paterno de Medida es obligatorio");
            }

            if (string.IsNullOrEmpty(txtSegundoApellido.Text))
            {
                esValido = false;
                erpApellidoPaterno.SetError(txtSegundoApellido, "El campo Apellido Paterno de Medida es obligatorio");
            }

            if (string.IsNullOrEmpty(cbxSexo.Text))
            {
                esValido = false;
                erpSexo.SetError(cbxSexo, "El campo Sexo es obligatorio");
            }

            if (string.IsNullOrEmpty(txtFecha.Text))
            {
                esValido = false;
                erpFechaContrato.SetError(txtFecha, "El campo Precio de Venta es obligatorio");
            }
            if (string.IsNullOrEmpty(cbxCargo.Text))
            {
                esValido = false;
                erpCargo.SetError(cbxCargo, "El campo Cantidad es obligatorio");
            }
            if (string.IsNullOrEmpty(txtCelular.Text))
            {
                esValido = false;
                erpCelular.SetError(txtCelular, "El campo Cantidad es obligatorio");
            }
            return esValido;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var empleado = new Empleado();
                empleado.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                empleado.nombre = txtNombre.Text.Trim();
                empleado.primerApellido = txtPrimerApellido.Text.Trim();
                empleado.segundoApellido = txtSegundoApellido.Text.Trim();
                empleado.sexo = cbxSexo.Text;
                empleado.fechaContrato = Convert.ToDateTime(txtFecha.Text);
                empleado.celular = Convert.ToInt64(txtCelular.Text);
                empleado.usuarioRegistro = "SIS457 - Musica";

                if (esNuevo)
                {
                    empleado.fechaRegistro = DateTime.Now;
                    empleado.estado = 1;
                    EmpleadoCln.insertar(empleado);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    empleado.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    EmpleadoCln.actualizar(empleado);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Empleado guardado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string cedulaIdentidad = dgvLista.Rows[index].Cells["cedulaIdentidad"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja al empleado {cedulaIdentidad}?",
                "::: Musica - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                EmpleadoCln.eliminar(id, "SIS457-Musica");
                listar();
                MessageBox.Show("Empleado eliminado correctamente", "::: Musica - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
    }
}
