using System;
using System.Windows.Forms;
using frmSistemaReserva.Modelos;
using frmSistemaReserva.AccesoDatos;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace frmSistemaReserva.InterfazUsuario
{
    public partial class frmNuevoPago : Form
    {
        private readonly Conexion conexion = new Conexion();
        private readonly int idReserva;
        private readonly string cliente;
        private readonly string habitacion;
        private readonly decimal costoTotal;



        public frmNuevoPago(int idReserva, string cliente, string habitacion, decimal costoTotal)
        {
            InitializeComponent();
            this.idReserva = idReserva;
            this.cliente = cliente;
            this.habitacion = habitacion;
            this.costoTotal = costoTotal;
        }

        private void frmNuevoPago_Load(object sender, EventArgs e)
        {
            this.Load += new System.EventHandler(this.frmNuevoPago_Load);

            // Inicializar los datos de la reserva
            lblIdReserva.Text = idReserva.ToString();
            lblCliente.Text = cliente;
            lblHabitacion.Text = habitacion;
            lblCostoTotal.Text = costoTotal.ToString("C");

            // Configurar los ComboBox
            cboMetodoPago.Items.AddRange(new string[] { "Efectivo", "Tarjeta de Crédito", "Transferencia Bancaria", "PayPal" });
            cboTipoDivisa.Items.AddRange(new string[] { "USD", "EUR", "GBP", "BTC" });

            cboMetodoPago.SelectedIndex = 0; // Selección por defecto
            cboTipoDivisa.SelectedIndex = 0; // Selección por defecto
        }

        private void btnGuardarPago_Click(object sender, EventArgs e)
        {
            // Validar que los campos estén completos
            if (string.IsNullOrEmpty(txtMonto.Text) || !decimal.TryParse(txtMonto.Text, out decimal montoPagado) || montoPagado <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear un objeto Pago con los datos capturados
            Pago nuevoPago = new Pago
            {
                IdReserva = idReserva,
                Monto = montoPagado,
                FechaPago = DateTime.Now,
                MetodoPago = cboMetodoPago.SelectedItem.ToString(),
                TipoDivisa = cboTipoDivisa.SelectedItem.ToString()
            };

            try
            {
                // Guardar el pago en la base de datos
                conexion.InsertarPagoConSP(nuevoPago);

                MessageBox.Show("Pago registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
