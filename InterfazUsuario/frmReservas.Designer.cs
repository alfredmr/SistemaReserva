namespace frmSistemaReserva.InterfazUsuario
{
    partial class frmReservas
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
            this.dgvReservas = new System.Windows.Forms.DataGridView();
            this.txtBuscarReserva = new System.Windows.Forms.TextBox();
            this.btnBuscarReserva = new System.Windows.Forms.Button();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDuiClientes = new System.Windows.Forms.ComboBox();
            this.txtTipoHabitacion = new System.Windows.Forms.TextBox();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.lblTipoHabitacion = new System.Windows.Forms.Label();
            this.lblNumeroHabitacion = new System.Windows.Forms.Label();
            this.cboNumeroHabitacion = new System.Windows.Forms.ComboBox();
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.btnNuevaReserva = new System.Windows.Forms.Button();
            this.grpDatosHabitacion = new System.Windows.Forms.GroupBox();
            this.dtpFechaInicioReserva = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinReserva = new System.Windows.Forms.DateTimePicker();
            this.grpDatosReserva = new System.Windows.Forms.GroupBox();
            this.btnListoEditar = new System.Windows.Forms.Button();
            this.lblFechaFinReserva = new System.Windows.Forms.Label();
            this.btnEditarReserva = new System.Windows.Forms.Button();
            this.lblFechaInicioReserva = new System.Windows.Forms.Label();
            this.btnEliminarReserva = new System.Windows.Forms.Button();
            this.btnGuardarReserva = new System.Windows.Forms.Button();
            this.btnCancelarReserva = new System.Windows.Forms.Button();
            this.btnSalirfrmReservas = new System.Windows.Forms.Button();
            this.grpHabitacionesDeMotel = new System.Windows.Forms.GroupBox();
            this.lbHabitaciones = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).BeginInit();
            this.grpDatosCliente.SuspendLayout();
            this.grpDatosHabitacion.SuspendLayout();
            this.grpDatosReserva.SuspendLayout();
            this.grpHabitacionesDeMotel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReservas
            // 
            this.dgvReservas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservas.Location = new System.Drawing.Point(44, 455);
            this.dgvReservas.Name = "dgvReservas";
            this.dgvReservas.ReadOnly = true;
            this.dgvReservas.Size = new System.Drawing.Size(964, 106);
            this.dgvReservas.TabIndex = 0;
            this.dgvReservas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReservas_CellClick);
            this.dgvReservas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReservas_CellClick);
            this.dgvReservas.SelectionChanged += new System.EventHandler(this.dgvReservas_SelectionChanged);
            // 
            // txtBuscarReserva
            // 
            this.txtBuscarReserva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarReserva.Location = new System.Drawing.Point(478, 416);
            this.txtBuscarReserva.Name = "txtBuscarReserva";
            this.txtBuscarReserva.Size = new System.Drawing.Size(259, 26);
            this.txtBuscarReserva.TabIndex = 1;
            // 
            // btnBuscarReserva
            // 
            this.btnBuscarReserva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarReserva.Location = new System.Drawing.Point(748, 409);
            this.btnBuscarReserva.Name = "btnBuscarReserva";
            this.btnBuscarReserva.Size = new System.Drawing.Size(93, 40);
            this.btnBuscarReserva.TabIndex = 2;
            this.btnBuscarReserva.Text = "Buscar";
            this.btnBuscarReserva.UseVisualStyleBackColor = true;
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Location = new System.Drawing.Point(126, 116);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(152, 26);
            this.txtNombreCliente.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "DUI:";
            // 
            // cboDuiClientes
            // 
            this.cboDuiClientes.FormattingEnabled = true;
            this.cboDuiClientes.Location = new System.Drawing.Point(126, 38);
            this.cboDuiClientes.Name = "cboDuiClientes";
            this.cboDuiClientes.Size = new System.Drawing.Size(152, 28);
            this.cboDuiClientes.TabIndex = 5;
            this.cboDuiClientes.Text = "Seleccione...";
            // 
            // txtTipoHabitacion
            // 
            this.txtTipoHabitacion.Location = new System.Drawing.Point(168, 113);
            this.txtTipoHabitacion.Name = "txtTipoHabitacion";
            this.txtTipoHabitacion.Size = new System.Drawing.Size(152, 26);
            this.txtTipoHabitacion.TabIndex = 6;
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Location = new System.Drawing.Point(30, 119);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(69, 20);
            this.lblNombreCliente.TabIndex = 7;
            this.lblNombreCliente.Text = "Nombre:";
            // 
            // lblTipoHabitacion
            // 
            this.lblTipoHabitacion.AutoSize = true;
            this.lblTipoHabitacion.Location = new System.Drawing.Point(24, 113);
            this.lblTipoHabitacion.Name = "lblTipoHabitacion";
            this.lblTipoHabitacion.Size = new System.Drawing.Size(43, 20);
            this.lblTipoHabitacion.TabIndex = 8;
            this.lblTipoHabitacion.Text = "Tipo:";
            // 
            // lblNumeroHabitacion
            // 
            this.lblNumeroHabitacion.AutoSize = true;
            this.lblNumeroHabitacion.Location = new System.Drawing.Point(24, 44);
            this.lblNumeroHabitacion.Name = "lblNumeroHabitacion";
            this.lblNumeroHabitacion.Size = new System.Drawing.Size(69, 20);
            this.lblNumeroHabitacion.TabIndex = 9;
            this.lblNumeroHabitacion.Text = "Número:";
            // 
            // cboNumeroHabitacion
            // 
            this.cboNumeroHabitacion.FormattingEnabled = true;
            this.cboNumeroHabitacion.Location = new System.Drawing.Point(168, 44);
            this.cboNumeroHabitacion.Name = "cboNumeroHabitacion";
            this.cboNumeroHabitacion.Size = new System.Drawing.Size(152, 28);
            this.cboNumeroHabitacion.TabIndex = 10;
            this.cboNumeroHabitacion.Text = "Seleccione...";
            // 
            // grpDatosCliente
            // 
            this.grpDatosCliente.Controls.Add(this.txtNombreCliente);
            this.grpDatosCliente.Controls.Add(this.label1);
            this.grpDatosCliente.Controls.Add(this.cboDuiClientes);
            this.grpDatosCliente.Controls.Add(this.lblNombreCliente);
            this.grpDatosCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosCliente.Location = new System.Drawing.Point(44, 22);
            this.grpDatosCliente.Name = "grpDatosCliente";
            this.grpDatosCliente.Size = new System.Drawing.Size(320, 165);
            this.grpDatosCliente.TabIndex = 13;
            this.grpDatosCliente.TabStop = false;
            this.grpDatosCliente.Text = "Datos del cliente";
            // 
            // btnNuevaReserva
            // 
            this.btnNuevaReserva.Location = new System.Drawing.Point(27, 36);
            this.btnNuevaReserva.Name = "btnNuevaReserva";
            this.btnNuevaReserva.Size = new System.Drawing.Size(93, 40);
            this.btnNuevaReserva.TabIndex = 15;
            this.btnNuevaReserva.Text = "Nuevo";
            this.btnNuevaReserva.UseVisualStyleBackColor = true;
            this.btnNuevaReserva.Click += new System.EventHandler(this.btnNuevaReserva_Click);
            // 
            // grpDatosHabitacion
            // 
            this.grpDatosHabitacion.Controls.Add(this.txtTipoHabitacion);
            this.grpDatosHabitacion.Controls.Add(this.lblTipoHabitacion);
            this.grpDatosHabitacion.Controls.Add(this.lblNumeroHabitacion);
            this.grpDatosHabitacion.Controls.Add(this.cboNumeroHabitacion);
            this.grpDatosHabitacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosHabitacion.Location = new System.Drawing.Point(374, 22);
            this.grpDatosHabitacion.Name = "grpDatosHabitacion";
            this.grpDatosHabitacion.Size = new System.Drawing.Size(363, 165);
            this.grpDatosHabitacion.TabIndex = 14;
            this.grpDatosHabitacion.TabStop = false;
            this.grpDatosHabitacion.Text = "Datos de la habitación";
            // 
            // dtpFechaInicioReserva
            // 
            this.dtpFechaInicioReserva.Location = new System.Drawing.Point(27, 135);
            this.dtpFechaInicioReserva.Name = "dtpFechaInicioReserva";
            this.dtpFechaInicioReserva.Size = new System.Drawing.Size(300, 26);
            this.dtpFechaInicioReserva.TabIndex = 15;
            // 
            // dtpFechaFinReserva
            // 
            this.dtpFechaFinReserva.Location = new System.Drawing.Point(381, 135);
            this.dtpFechaFinReserva.Name = "dtpFechaFinReserva";
            this.dtpFechaFinReserva.Size = new System.Drawing.Size(300, 26);
            this.dtpFechaFinReserva.TabIndex = 16;
            // 
            // grpDatosReserva
            // 
            this.grpDatosReserva.Controls.Add(this.btnListoEditar);
            this.grpDatosReserva.Controls.Add(this.lblFechaFinReserva);
            this.grpDatosReserva.Controls.Add(this.btnEditarReserva);
            this.grpDatosReserva.Controls.Add(this.lblFechaInicioReserva);
            this.grpDatosReserva.Controls.Add(this.btnEliminarReserva);
            this.grpDatosReserva.Controls.Add(this.btnGuardarReserva);
            this.grpDatosReserva.Controls.Add(this.btnCancelarReserva);
            this.grpDatosReserva.Controls.Add(this.btnNuevaReserva);
            this.grpDatosReserva.Controls.Add(this.dtpFechaInicioReserva);
            this.grpDatosReserva.Controls.Add(this.dtpFechaFinReserva);
            this.grpDatosReserva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosReserva.Location = new System.Drawing.Point(44, 209);
            this.grpDatosReserva.Name = "grpDatosReserva";
            this.grpDatosReserva.Size = new System.Drawing.Size(693, 177);
            this.grpDatosReserva.TabIndex = 17;
            this.grpDatosReserva.TabStop = false;
            this.grpDatosReserva.Text = "Datos reserva";
            // 
            // btnListoEditar
            // 
            this.btnListoEditar.Location = new System.Drawing.Point(524, 36);
            this.btnListoEditar.Name = "btnListoEditar";
            this.btnListoEditar.Size = new System.Drawing.Size(93, 40);
            this.btnListoEditar.TabIndex = 22;
            this.btnListoEditar.Text = "Listo";
            this.btnListoEditar.UseVisualStyleBackColor = true;
            this.btnListoEditar.Click += new System.EventHandler(this.btnListoEditar_Click);
            // 
            // lblFechaFinReserva
            // 
            this.lblFechaFinReserva.AutoSize = true;
            this.lblFechaFinReserva.Location = new System.Drawing.Point(377, 112);
            this.lblFechaFinReserva.Name = "lblFechaFinReserva";
            this.lblFechaFinReserva.Size = new System.Drawing.Size(135, 20);
            this.lblFechaFinReserva.TabIndex = 19;
            this.lblFechaFinReserva.Text = "Fecha fin reserva:";
            // 
            // btnEditarReserva
            // 
            this.btnEditarReserva.Location = new System.Drawing.Point(326, 36);
            this.btnEditarReserva.Name = "btnEditarReserva";
            this.btnEditarReserva.Size = new System.Drawing.Size(93, 40);
            this.btnEditarReserva.TabIndex = 21;
            this.btnEditarReserva.Text = "Editar";
            this.btnEditarReserva.UseVisualStyleBackColor = true;
            this.btnEditarReserva.Click += new System.EventHandler(this.btnEditarReserva_Click);
            // 
            // lblFechaInicioReserva
            // 
            this.lblFechaInicioReserva.AutoSize = true;
            this.lblFechaInicioReserva.Location = new System.Drawing.Point(23, 112);
            this.lblFechaInicioReserva.Name = "lblFechaInicioReserva";
            this.lblFechaInicioReserva.Size = new System.Drawing.Size(155, 20);
            this.lblFechaInicioReserva.TabIndex = 18;
            this.lblFechaInicioReserva.Text = "Fecha Inicio reserva:";
            // 
            // btnEliminarReserva
            // 
            this.btnEliminarReserva.Location = new System.Drawing.Point(425, 36);
            this.btnEliminarReserva.Name = "btnEliminarReserva";
            this.btnEliminarReserva.Size = new System.Drawing.Size(93, 40);
            this.btnEliminarReserva.TabIndex = 20;
            this.btnEliminarReserva.Text = "Eliminar";
            this.btnEliminarReserva.UseVisualStyleBackColor = true;
            this.btnEliminarReserva.Click += new System.EventHandler(this.btnEliminarReserva_Click);
            // 
            // btnGuardarReserva
            // 
            this.btnGuardarReserva.Location = new System.Drawing.Point(227, 36);
            this.btnGuardarReserva.Name = "btnGuardarReserva";
            this.btnGuardarReserva.Size = new System.Drawing.Size(93, 40);
            this.btnGuardarReserva.TabIndex = 19;
            this.btnGuardarReserva.Text = "Guardar";
            this.btnGuardarReserva.UseVisualStyleBackColor = true;
            this.btnGuardarReserva.Click += new System.EventHandler(this.btnGuardarReserva_Click);
            // 
            // btnCancelarReserva
            // 
            this.btnCancelarReserva.Location = new System.Drawing.Point(126, 36);
            this.btnCancelarReserva.Name = "btnCancelarReserva";
            this.btnCancelarReserva.Size = new System.Drawing.Size(93, 40);
            this.btnCancelarReserva.TabIndex = 18;
            this.btnCancelarReserva.Text = "Cancelar";
            this.btnCancelarReserva.UseVisualStyleBackColor = true;
            this.btnCancelarReserva.Click += new System.EventHandler(this.btnCancelarReserva_Click);
            // 
            // btnSalirfrmReservas
            // 
            this.btnSalirfrmReservas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalirfrmReservas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalirfrmReservas.Location = new System.Drawing.Point(1014, 521);
            this.btnSalirfrmReservas.Name = "btnSalirfrmReservas";
            this.btnSalirfrmReservas.Size = new System.Drawing.Size(93, 40);
            this.btnSalirfrmReservas.TabIndex = 18;
            this.btnSalirfrmReservas.Text = "Salir";
            this.btnSalirfrmReservas.UseVisualStyleBackColor = true;
            this.btnSalirfrmReservas.Click += new System.EventHandler(this.btnSalirfrmReservas_Click);
            // 
            // grpHabitacionesDeMotel
            // 
            this.grpHabitacionesDeMotel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHabitacionesDeMotel.Controls.Add(this.lbHabitaciones);
            this.grpHabitacionesDeMotel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpHabitacionesDeMotel.Location = new System.Drawing.Point(743, 22);
            this.grpHabitacionesDeMotel.Name = "grpHabitacionesDeMotel";
            this.grpHabitacionesDeMotel.Size = new System.Drawing.Size(364, 364);
            this.grpHabitacionesDeMotel.TabIndex = 19;
            this.grpHabitacionesDeMotel.TabStop = false;
            this.grpHabitacionesDeMotel.Text = "Habitaciones del hostal";
            // 
            // lbHabitaciones
            // 
            this.lbHabitaciones.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHabitaciones.FormattingEnabled = true;
            this.lbHabitaciones.ItemHeight = 19;
            this.lbHabitaciones.Location = new System.Drawing.Point(6, 31);
            this.lbHabitaciones.Name = "lbHabitaciones";
            this.lbHabitaciones.Size = new System.Drawing.Size(352, 327);
            this.lbHabitaciones.TabIndex = 1;
            this.lbHabitaciones.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbHabitaciones_DrawItem);
            // 
            // frmReservas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 585);
            this.Controls.Add(this.grpHabitacionesDeMotel);
            this.Controls.Add(this.btnSalirfrmReservas);
            this.Controls.Add(this.grpDatosReserva);
            this.Controls.Add(this.grpDatosHabitacion);
            this.Controls.Add(this.grpDatosCliente);
            this.Controls.Add(this.btnBuscarReserva);
            this.Controls.Add(this.txtBuscarReserva);
            this.Controls.Add(this.dgvReservas);
            this.Name = "frmReservas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de reservas";
            this.Load += new System.EventHandler(this.frmReservas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).EndInit();
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.grpDatosHabitacion.ResumeLayout(false);
            this.grpDatosHabitacion.PerformLayout();
            this.grpDatosReserva.ResumeLayout(false);
            this.grpDatosReserva.PerformLayout();
            this.grpHabitacionesDeMotel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReservas;
        private System.Windows.Forms.TextBox txtBuscarReserva;
        private System.Windows.Forms.Button btnBuscarReserva;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDuiClientes;
        private System.Windows.Forms.TextBox txtTipoHabitacion;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.Label lblTipoHabitacion;
        private System.Windows.Forms.Label lblNumeroHabitacion;
        private System.Windows.Forms.ComboBox cboNumeroHabitacion;
        private System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.GroupBox grpDatosHabitacion;
        private System.Windows.Forms.Button btnNuevaReserva;
        private System.Windows.Forms.DateTimePicker dtpFechaInicioReserva;
        private System.Windows.Forms.DateTimePicker dtpFechaFinReserva;
        private System.Windows.Forms.GroupBox grpDatosReserva;
        private System.Windows.Forms.Button btnEliminarReserva;
        private System.Windows.Forms.Button btnCancelarReserva;
        private System.Windows.Forms.Button btnEditarReserva;
        private System.Windows.Forms.Button btnGuardarReserva;
        private System.Windows.Forms.Label lblFechaFinReserva;
        private System.Windows.Forms.Label lblFechaInicioReserva;
        private System.Windows.Forms.Button btnSalirfrmReservas;
        private System.Windows.Forms.GroupBox grpHabitacionesDeMotel;
        private System.Windows.Forms.ListBox lbHabitaciones;
        private System.Windows.Forms.Button btnListoEditar;
    }
}