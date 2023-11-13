namespace CpMusica
{
    partial class FrmControlNuevo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControlNuevo));
            this.btnUsuario = new System.Windows.Forms.Button();
            this.btnEmpleado = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUsuario
            // 
            this.btnUsuario.ForeColor = System.Drawing.Color.Black;
            this.btnUsuario.Image = global::CpMusica.Properties.Resources._new;
            this.btnUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuario.Location = new System.Drawing.Point(31, 100);
            this.btnUsuario.Name = "btnUsuario";
            this.btnUsuario.Size = new System.Drawing.Size(133, 42);
            this.btnUsuario.TabIndex = 9;
            this.btnUsuario.Text = "Nuevo Usuario";
            this.btnUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUsuario.UseVisualStyleBackColor = true;
            this.btnUsuario.Click += new System.EventHandler(this.btnUsuario_Click);
            // 
            // btnEmpleado
            // 
            this.btnEmpleado.ForeColor = System.Drawing.Color.Black;
            this.btnEmpleado.Image = global::CpMusica.Properties.Resources._new;
            this.btnEmpleado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmpleado.Location = new System.Drawing.Point(288, 100);
            this.btnEmpleado.Name = "btnEmpleado";
            this.btnEmpleado.Size = new System.Drawing.Size(128, 42);
            this.btnEmpleado.TabIndex = 8;
            this.btnEmpleado.Text = "Nuevo Empleado";
            this.btnEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEmpleado.UseVisualStyleBackColor = true;
            this.btnEmpleado.Click += new System.EventHandler(this.btnEmpleado_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(35, 9);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(394, 28);
            this.lblTitulo.TabIndex = 25;
            this.lblTitulo.Text = "CREAR DATOS Y ACCESOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmControlNuevo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(452, 170);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnUsuario);
            this.Controls.Add(this.btnEmpleado);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmControlNuevo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: Crear Nuevos Accesos - Musica :::";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmControlNuevo_FormClosing);
            this.Load += new System.EventHandler(this.FrmControlNuevo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEmpleado;
        private System.Windows.Forms.Button btnUsuario;
        private System.Windows.Forms.Label lblTitulo;
    }
}