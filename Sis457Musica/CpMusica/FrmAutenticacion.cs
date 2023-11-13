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
    public partial class FrmAutenticacion : Form
    {
        public FrmAutenticacion()
        {
            InitializeComponent();
        }

        

        private void FrmAutenticacion_Load(object sender, EventArgs e)
        {
            Size = new Size(443, 244);
        }

        

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private bool validar()
        {
            bool esValido = true;
            erpUsuario.SetError(txtUsuario, "");
            erpClave.SetError(txtClave, "");
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                erpUsuario.SetError(txtUsuario, "El campo usuario es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                erpClave.SetError(txtClave, "El campo contraseña es obligatorio");
                esValido = false;
            }
            return esValido;
        }


        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            if (validar())
            {
                var usuario = UsuarioCln.validar(txtUsuario.Text, Util.Encrypt(txtClave.Text));
                if (usuario != null)
                {
                    Util.usuario = usuario;
                    txtClave.Text = string.Empty;
                    txtUsuario.Focus();
                    txtUsuario.SelectAll();
                    Visible = false;
                    new FrmArticulo(this).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrectos",
                        "::: Musica - Mensaje :::", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        
















        private bool validar2()
        {
            bool esValido = true;
            erpAcceso.SetError(txtAcceso, "");
            if (string.IsNullOrEmpty(txtAcceso.Text))
            {
                erpAcceso.SetError(txtAcceso, "No Insertarste nigun Codigo");
                esValido = false;
            }
            return esValido;
        }

        private void liblCrearUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Size = new Size(443, 313);
            txtAcceso.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(443, 244);
            
        }



        private void btnAcceso_Click(object sender, EventArgs e)
        {
            if (validar2())
            {
                var acceso = AccesoAdminCln.validar2(txtAcceso.Text);
                if (acceso != null)
                {
                    txtAcceso.Text = string.Empty;
                    txtAcceso.Focus();
                    txtAcceso.SelectAll();
                    Visible = false;
                    new FrmControlNuevo(this).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Codigo Incorrecto",
                        "::: Musica - Mensaje :::", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
