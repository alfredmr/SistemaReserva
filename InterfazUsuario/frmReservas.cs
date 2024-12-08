using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using frmSistemaReserva.AccesoDatos;
using frmSistemaReserva.Modelos;

namespace frmSistemaReserva.InterfazUsuario
{
    public partial class frmReservas : Form
    {
        //private Reserva reserva = new Reserva();
        Conexion conexion = new Conexion();
        public frmReservas()
        {
            InitializeComponent();           
        }

        private void frmReservas_Load(object sender, EventArgs e)
        {
            DesactivarBotones();
            CargarReservas();
            CargarListaDui();
            CargarListaNumeroHabitacion();
            cboDuiClientes.Text = "Seleccione...";
            cboNumeroHabitacion.Text = "Seleccione...";
        }

        private void CargarReservas()
        {
            try
            {
                List<Reserva> reservas = conexion.ObtenerReservas();

                dgvReservas.AutoGenerateColumns = true;  // Habilitar la generación automática de columnas
                dgvReservas.DataSource = reservas;       // Asignar la lista de usuarios al DataGridView
                dgvReservas.Refresh();                   // Refrescar el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de reservas" + ex.Message);
            }
            
        }

        private void CargarListaDui()
        {
            Reserva listaDui = new Reserva();
            cboDuiClientes.DataSource = conexion.ListarDuiClientes();
            cboDuiClientes.ValueMember = "idCliente";
            cboDuiClientes.DisplayMember = "nNumeroIdentificacion";
            cboDuiClientes.SelectedIndexChanged += (sender, e) =>
            {
                if (cboDuiClientes.SelectedIndex >= 0)
                {
                    // Obtener el idCliente seleccionado
                    int idSeleccionado = (int)cboDuiClientes.SelectedValue;

                    // Obtener el nombre completo del cliente seleccionado
                    string nombreCompleto = conexion.ObtenerNombreporId(idSeleccionado);

                    // Asignar el nombre completo al TextBox
                    txtNombreCliente.Text = nombreCompleto;
                }
            };
            
        }

        private void CargarListaNumeroHabitacion()
        {
            //Reserva listaNumero = new Reserva();
            cboNumeroHabitacion.DataSource = conexion.ListarNumeroHabitaciones();
            cboNumeroHabitacion.ValueMember = "IdHabitacion";
            cboNumeroHabitacion.DisplayMember = "numeroHabitacion";
            cboNumeroHabitacion.SelectedIndexChanged += (sender, e) =>
            {
                if (cboNumeroHabitacion.SelectedIndex >= 0)
                {
                    // Obtener el idCliente seleccionado
                    int idSeleccionado = (int)cboNumeroHabitacion.SelectedValue;

                    // Obtener el nombre completo del cliente seleccionado
                    string numeroHabitacion = conexion.ObtenerTipoPorId(idSeleccionado);

                    // Asignar el nombre completo al TextBox
                    txtTipoHabitacion.Text = numeroHabitacion;
                }
            };

        }

        private void ActivarBotones()
        {
            btnCancelarReserva.Enabled = true;
            btnGuardarREserva.Enabled = true;
            btnEditarReserva.Enabled = true;
            btnEliminarReserva.Enabled = true;
            btnNuevaReserva.Enabled = false;
            cboDuiClientes.Enabled = true;
            cboNumeroHabitacion.Enabled = true;
            txtNombreCliente.Enabled = true;
            txtTipoHabitacion.Enabled = true;
            dtpFechaInicioReserva.Enabled = true;
            dtpFechaFinReserva.Enabled = true;
        }

        private void DesactivarBotones()
        {
            btnCancelarReserva.Enabled = false;
            btnGuardarREserva.Enabled = false;
            btnEditarReserva.Enabled = false;
            btnEliminarReserva.Enabled = false;
            btnNuevaReserva.Enabled = true;
            cboDuiClientes.Text = "Seleccione...";
            cboDuiClientes.Enabled = false;
            cboNumeroHabitacion.Text = "Seleccione...";
            cboNumeroHabitacion.Enabled = false;
            txtNombreCliente.Text = " ";
            txtNombreCliente.Enabled = false;
            txtTipoHabitacion.Text = " ";
            txtTipoHabitacion.Enabled = false;
            dtpFechaInicioReserva.Enabled = false;
            dtpFechaFinReserva.Enabled = false;
        }

        private void btnNuevaReserva_Click(object sender, EventArgs e)
        {
            ActivarBotones();
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            DesactivarBotones();
        }


        private void btnGuardarREserva_Click(object sender, EventArgs e)
        {
            // Crear un nuevo usuario con los datos ingresados en el formulario
            Reserva nuevaReserva = new Reserva()
            {
                IdCliente = Convert.ToInt32(cboDuiClientes.SelectedValue),
                IdHabitacion = Convert.ToInt32(cboNumeroHabitacion.SelectedValue),
                IdUsuario = 4,
                FechaInicio = dtpFechaInicioReserva.Value,
                FechaFin = dtpFechaFinReserva.Value,
            };

            // Validación de campos vacíos
            if (string.IsNullOrEmpty(Convert.ToString(nuevaReserva.IdCliente)) || string.IsNullOrEmpty(Convert.ToString(nuevaReserva.IdHabitacion)) ||
                string.IsNullOrEmpty(Convert.ToString(nuevaReserva.IdUsuario)) || nuevaReserva.FechaInicio == null ||
                nuevaReserva.FechaInicio.ToString() == "" || nuevaReserva.FechaFin == null ||
                nuevaReserva.FechaInicio.ToString() == "")
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insertar usuario en la base de datos utilizando el procedimiento almacenado
            Conexion dbHelper = new Conexion();
            try
            {
                dbHelper.InsertarReservaConSP(nuevaReserva);
                MessageBox.Show("Usuario agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarReservas(); // Refrescar la lista de usuarios
                //LimpiarCampos(); // Limpiar y deshabilitar los campos después de agregar
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("El usuario ya existe. Por favor, intente con un nombre de usuario diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Error al agregar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarReserva_Click(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow != null)
            {
                // Obtener el Id del usuario seleccionado
                int idReserva = Convert.ToInt32(dgvReservas.CurrentRow.Cells["IdReserva"].Value);

                // Confirmar la eliminación
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    // Eliminar el usuario de la base de datos
                    Reserva reserva = new Reserva();
                    try
                    {
                        conexion.EliminarReservaConSP(idReserva);
                        MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Actualizar la lista de usuarios
                        CargarReservas();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error al eliminar reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione reserva para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalirfrmReservas_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
