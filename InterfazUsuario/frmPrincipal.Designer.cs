namespace frmSistemaReserva.InterfazUsuario
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.btnSingOut = new System.Windows.Forms.Button();
            this.gpbInfoUsuario = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbModulos = new System.Windows.Forms.GroupBox();
            this.btnPagos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnReservas = new System.Windows.Forms.Button();
            this.lblReservas = new System.Windows.Forms.Label();
            this.btnHabitaciones = new System.Windows.Forms.Button();
            this.lblHabitaciones = new System.Windows.Forms.Label();
            this.btnClientes = new System.Windows.Forms.Button();
            this.lvlClientes = new System.Windows.Forms.Label();
            this.gpbInfoUsuario.SuspendLayout();
            this.gbModulos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUsuarios.AutoSize = true;
            this.lblUsuarios.Location = new System.Drawing.Point(158, 595);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(72, 20);
            this.lblUsuarios.TabIndex = 1;
            this.lblUsuarios.Text = "Usuarios";
            // 
            // btnSingOut
            // 
            this.btnSingOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSingOut.AutoEllipsis = true;
            this.btnSingOut.BackColor = System.Drawing.Color.IndianRed;
            this.btnSingOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSingOut.ForeColor = System.Drawing.Color.Transparent;
            this.btnSingOut.Location = new System.Drawing.Point(864, 674);
            this.btnSingOut.Name = "btnSingOut";
            this.btnSingOut.Size = new System.Drawing.Size(120, 35);
            this.btnSingOut.TabIndex = 0;
            this.btnSingOut.Text = "Cerrar Sesión";
            this.btnSingOut.UseVisualStyleBackColor = false;
            this.btnSingOut.Click += new System.EventHandler(this.btnSingOut_Click);
            // 
            // gpbInfoUsuario
            // 
            this.gpbInfoUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gpbInfoUsuario.Controls.Add(this.lblUsuario);
            this.gpbInfoUsuario.Controls.Add(this.label1);
            this.gpbInfoUsuario.Location = new System.Drawing.Point(20, 20);
            this.gpbInfoUsuario.Name = "gpbInfoUsuario";
            this.gpbInfoUsuario.Size = new System.Drawing.Size(300, 634);
            this.gpbInfoUsuario.TabIndex = 3;
            this.gpbInfoUsuario.TabStop = false;
            this.gpbInfoUsuario.Text = "Usuario";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(6, 85);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(100, 25);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "{Usuario}";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bienvenido!!";
            // 
            // gbModulos
            // 
            this.gbModulos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbModulos.Controls.Add(this.btnPagos);
            this.gbModulos.Controls.Add(this.label2);
            this.gbModulos.Controls.Add(this.btnUsuarios);
            this.gbModulos.Controls.Add(this.lblUsuarios);
            this.gbModulos.Controls.Add(this.btnReservas);
            this.gbModulos.Controls.Add(this.lblReservas);
            this.gbModulos.Controls.Add(this.btnHabitaciones);
            this.gbModulos.Controls.Add(this.lblHabitaciones);
            this.gbModulos.Controls.Add(this.btnClientes);
            this.gbModulos.Controls.Add(this.lvlClientes);
            this.gbModulos.Location = new System.Drawing.Point(326, 20);
            this.gbModulos.Name = "gbModulos";
            this.gbModulos.Size = new System.Drawing.Size(658, 634);
            this.gbModulos.TabIndex = 4;
            this.gbModulos.TabStop = false;
            this.gbModulos.Text = "Módulos";
            // 
            // btnPagos
            // 
            this.btnPagos.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnPagos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPagos.BackgroundImage = global::frmSistemaReserva.Properties.Resources.dar_dinero;
            this.btnPagos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPagos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagos.ForeColor = System.Drawing.Color.Transparent;
            this.btnPagos.Location = new System.Drawing.Point(265, 32);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Size = new System.Drawing.Size(100, 100);
            this.btnPagos.TabIndex = 10;
            this.btnPagos.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Pagos";
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUsuarios.AutoEllipsis = true;
            this.btnUsuarios.BackgroundImage = global::frmSistemaReserva.Properties.Resources.gestion;
            this.btnUsuarios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuarios.ForeColor = System.Drawing.Color.Transparent;
            this.btnUsuarios.Location = new System.Drawing.Point(144, 492);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(100, 100);
            this.btnUsuarios.TabIndex = 0;
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnReservas
            // 
            this.btnReservas.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnReservas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReservas.BackgroundImage = global::frmSistemaReserva.Properties.Resources.reserva;
            this.btnReservas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReservas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReservas.ForeColor = System.Drawing.Color.Transparent;
            this.btnReservas.Location = new System.Drawing.Point(144, 32);
            this.btnReservas.Name = "btnReservas";
            this.btnReservas.Size = new System.Drawing.Size(100, 100);
            this.btnReservas.TabIndex = 8;
            this.btnReservas.UseVisualStyleBackColor = true;
            this.btnReservas.Click += new System.EventHandler(this.btnReservas_Click);
            // 
            // lblReservas
            // 
            this.lblReservas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReservas.AutoSize = true;
            this.lblReservas.Location = new System.Drawing.Point(160, 135);
            this.lblReservas.Name = "lblReservas";
            this.lblReservas.Size = new System.Drawing.Size(68, 20);
            this.lblReservas.TabIndex = 9;
            this.lblReservas.Text = "Reserva";
            // 
            // btnHabitaciones
            // 
            this.btnHabitaciones.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnHabitaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHabitaciones.BackgroundImage = global::frmSistemaReserva.Properties.Resources.silencio;
            this.btnHabitaciones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHabitaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHabitaciones.ForeColor = System.Drawing.Color.Transparent;
            this.btnHabitaciones.Location = new System.Drawing.Point(23, 492);
            this.btnHabitaciones.Name = "btnHabitaciones";
            this.btnHabitaciones.Size = new System.Drawing.Size(100, 100);
            this.btnHabitaciones.TabIndex = 6;
            this.btnHabitaciones.UseVisualStyleBackColor = true;
            // 
            // lblHabitaciones
            // 
            this.lblHabitaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHabitaciones.AutoSize = true;
            this.lblHabitaciones.Location = new System.Drawing.Point(22, 595);
            this.lblHabitaciones.Name = "lblHabitaciones";
            this.lblHabitaciones.Size = new System.Drawing.Size(102, 20);
            this.lblHabitaciones.TabIndex = 7;
            this.lblHabitaciones.Text = "Habitaciones";
            // 
            // btnClientes
            // 
            this.btnClientes.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClientes.BackgroundImage = global::frmSistemaReserva.Properties.Resources.cliente;
            this.btnClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClientes.ForeColor = System.Drawing.Color.Transparent;
            this.btnClientes.Location = new System.Drawing.Point(23, 32);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(100, 100);
            this.btnClientes.TabIndex = 4;
            this.btnClientes.UseVisualStyleBackColor = true;
            // 
            // lvlClientes
            // 
            this.lvlClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lvlClientes.AutoSize = true;
            this.lvlClientes.Location = new System.Drawing.Point(40, 135);
            this.lvlClientes.Name = "lvlClientes";
            this.lvlClientes.Size = new System.Drawing.Size(66, 20);
            this.lvlClientes.TabIndex = 5;
            this.lvlClientes.Text = "Clientes";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.ControlBox = false;
            this.Controls.Add(this.gbModulos);
            this.Controls.Add(this.btnSingOut);
            this.Controls.Add(this.gpbInfoUsuario);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Principal";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.gpbInfoUsuario.ResumeLayout(false);
            this.gpbInfoUsuario.PerformLayout();
            this.gbModulos.ResumeLayout(false);
            this.gbModulos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Button btnSingOut;
        private System.Windows.Forms.GroupBox gpbInfoUsuario;
        private System.Windows.Forms.GroupBox gbModulos;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReservas;
        private System.Windows.Forms.Label lblReservas;
        private System.Windows.Forms.Button btnHabitaciones;
        private System.Windows.Forms.Label lblHabitaciones;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Label lvlClientes;
        private System.Windows.Forms.Button btnPagos;
        private System.Windows.Forms.Label label2;
    }
}