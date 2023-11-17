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
        public FrmPrincipal()
        {
            InitializeComponent();
            hideSubMenu();
            botonRegistro();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void botonRegistro()
        {

            btnRegistro.Visible = true;
        }

        private void hideSubMenu()
        {
            pnlSubMenuRegistro.Visible = false;
           // panelPlaylistSubMenu.Visible = false;
            //panelToolsSubMenu.Visible = false;
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
            new FrmPrincipal().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FrmArticulo().ShowDialog();
        }

       
    }
}
