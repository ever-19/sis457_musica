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
    public partial class FrmPrincipal : Form
    {
        FrmAutenticacion frmAutenticacion;
        public FrmPrincipal(FrmAutenticacion frmAutenticacion)
        {
            InitializeComponent();
            hideSubMenu();
            botonRegistro();
            this.frmAutenticacion = frmAutenticacion;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        
        private void botonRegistro()
        {
            
            btnRegistro.Visible = false;
        }

        private void hideSubMenu()
        {
            pnlSubMenuRegistro.Visible = false;

        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            {
                showSubMenu(pnlSubMenuRegistro);
            }
        }

        private Form activeForm = null;
        
       
        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            new FrmEmpleado().ShowDialog();

            hideSubMenu();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            new FrmUsuario().ShowDialog();
            hideSubMenu();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            //new FrmPrincipal().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FrmArticulo().ShowDialog();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            new FrmCategoria().ShowDialog();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAutenticacion.Visible = true;
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            new FrmCliente().ShowDialog();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            new FrmVenta().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FrmVentaDetalle().ShowDialog();
        }

       
    }
}
