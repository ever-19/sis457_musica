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
    public partial class FrmControlNuevo : Form
    {
        FrmAutenticacion frmAutenticacion;
        public FrmControlNuevo(FrmAutenticacion frmAutenticacion)
        {
            InitializeComponent();
            this.frmAutenticacion = frmAutenticacion;
        }

        private void FrmControlNuevo_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAutenticacion.Visible = true;
        }



        public FrmControlNuevo()
        {
            InitializeComponent();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            new FrmUsuario().ShowDialog();
        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            new FrmEmpleado().ShowDialog();
        }

        private void FrmControlNuevo_Load(object sender, EventArgs e)
        {

        }

        
    }
}
