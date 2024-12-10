using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using frmSistemaReserva.AccesoDatos;
using frmSistemaReserva.Modelos;
using System.Linq;

namespace frmSistemaReserva.InterfazUsuario
{
    public partial class frmReservas : Form
    {
        //private Reserva reserva = new Reserva();
        Conexion conexion = new Conexion();
        private int idUsuario;
        public frmReservas(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
        }

        private void frmReservas_Load(object sender, EventArgs e)
        {
            DesactivarBotones();
            CargarReservas();
            CargarListaDui();
            CargarListaNumeroHabitacion();
            CargarHabitacionesEnListBox();
            cboDuiClientes.Text = "Seleccione...";
            cboNumeroHabitacion.Text = "Seleccione...";
            dtpFechaFinReserva.Value = DateTime.Now.AddDays(1);
            btnEditarReserva.Enabled = true;


            lbHabitaciones.DrawItem += lbHabitaciones_DrawItem;
            lbHabitaciones.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void CargarReservas()
        {
            try
            {
                List<viewReserva> reservas = conexion.ObtenerReservas();

                dgvReservas.AutoGenerateColumns = true;
                dgvReservas.DataSource = reservas;
                dgvReservas.Refresh();
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

            // Si deseas llenar el nombre completo en un TextBox al seleccionar un cliente
            cboDuiClientes.SelectedIndexChanged += (sender, e) =>
            {
                if (cboDuiClientes.SelectedIndex >= 0)
                {
                    DataRowView row = (DataRowView)cboDuiClientes.SelectedItem;
                    txtNombreCliente.Text = row["nombreCompleto"].ToString();
                }
            };
        }

        /*
        private void CargarListaNumeroHabitacion()
        {
            // Obtener todas las habitaciones
            DataTable habitaciones = conexion.ListarNumeroHabitaciones();

            if (habitaciones != null)
            {
                // Filtrar habitaciones diferentes a "Ocupada"
                DataRow[] filasFiltradas = habitaciones.Select("estado <> 'Ocupada'");

                if (filasFiltradas.Length > 0)
                {
                    // Crear un nuevo DataTable a partir de las filas filtradas
                    DataTable habitacionesFiltradas = filasFiltradas.CopyToDataTable();

                    // Asignar el DataTable filtrado al ComboBox
                    cboNumeroHabitacion.DataSource = habitacionesFiltradas;
                    cboNumeroHabitacion.ValueMember = "IdHabitacion";
                    cboNumeroHabitacion.DisplayMember = "numeroHabitacion";

                // Evento para actualizar el tipo de habitación y el precio al seleccionar una habitación
                cboNumeroHabitacion.SelectedIndexChanged += (sender, e) =>
                {
                    if (cboNumeroHabitacion.SelectedIndex >= 0)
                    {
                        DataRowView row = (DataRowView)cboNumeroHabitacion.SelectedItem;

                        // Actualizar el tipo de habitación en un TextBox
                        txtTipoHabitacion.Text = row["tipo"].ToString();

                        // Guardar el precio por noche en el Tag (o actualizar otro control si es necesario)
                        txtTipoHabitacion.Tag = row["precioPorNoche"];
                    }
                };
            }
            else
            {
                MessageBox.Show("No hay habitaciones disponibles para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }*/

        
        private void CargarListaNumeroHabitacion()
        {
            //Reserva listaNumero = new Reserva();
            cboNumeroHabitacion.DataSource = conexion.ListarNumeroHabitaciones();
            cboNumeroHabitacion.ValueMember = "IdHabitacion";
            cboNumeroHabitacion.DisplayMember = "numeroHabitacion";

            // Si deseas llenar el nombre completo en un TextBox al seleccionar un cliente
            cboNumeroHabitacion.SelectedIndexChanged += (sender, e) =>
            {
                if (cboNumeroHabitacion.SelectedIndex >= 0)
                {
                    DataRowView row = (DataRowView)cboNumeroHabitacion.SelectedItem;
                    txtTipoHabitacion.Text = row["tipo"].ToString();
                    // Almacena el precio por noche en el control (puede ser en el Tag).
                    txtTipoHabitacion.Tag = row["precioPorNoche"]; // Guarda el precio en el Tag.

                }
            };
        }

        private void CargarHabitacionesEnListBox()
        {
            try
            {
                // Obtener las habitaciones disponibles desde la base de datos
                DataTable habitaciones = conexion.ListarNumeroHabitaciones();
                lbHabitaciones.Items.Clear();

                // Añadir los datos al ListBox
                foreach (DataRow row in habitaciones.Rows)
                {
                    string numeroHabitacion = row["numeroHabitacion"].ToString();
                    string tipo = row["tipo"].ToString();
                    string estado = row["estado"].ToString();

                    // Crear una línea formateada con columnas de ancho fijo
                    string item = string.Format("{0,-5} {1,-20} {2,-10}", numeroHabitacion, tipo, estado);
                    lbHabitaciones.Items.Add(new ListItem { Text = item, Estado = estado });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar habitaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void lbHabitaciones_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ListBox listBox = (ListBox)sender;
            ListItem item = (ListItem)listBox.Items[e.Index];

            // Establecer colores según el estado
            Color textColor = item.Estado == "Ocupada" ? Color.Red : Color.Black;

            // Dibujar fondo
            e.DrawBackground();
            using (Brush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(item.Text, e.Font, textBrush, e.Bounds);
            }

            // Dibujar el borde de enfoque si está seleccionado
            e.DrawFocusRectangle();
        }

        private void ActivarBotones()
        {
            btnCancelarReserva.Enabled = true;
            btnGuardarReserva.Enabled = true;
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
            btnGuardarReserva.Enabled = false;
            btnEditarReserva.Visible = false;
            btnEliminarReserva.Visible = false;
            btnNuevaReserva.Enabled = true;
            cboDuiClientes.Text = "Seleccione...";
            cboDuiClientes.Enabled = false;
            cboNumeroHabitacion.Text = "Seleccione...";
            cboNumeroHabitacion.Enabled = false;
            txtNombreCliente.Clear();
            txtNombreCliente.Enabled = false;
            txtTipoHabitacion.Clear();
            txtTipoHabitacion.Enabled = false;
            dtpFechaInicioReserva.Enabled = false;
            dtpFechaFinReserva.Enabled = false;
            btnListoEditar.Enabled = false;
            btnListoEditar.Visible = false;

        }

        private void btnNuevaReserva_Click(object sender, EventArgs e)
        {
            DesactivarBotones();
            ActivarBotones();
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            DesactivarBotones();
            btnEditarReserva.Enabled = true;
        }

        private void btnGuardarReserva_Click(object sender, EventArgs e)
        {
            // Crear una nueva reserva con los datos del formulario
            Reserva nuevaReserva = new Reserva()
            {
                IdCliente = Convert.ToInt32(cboDuiClientes.SelectedValue),
                Cliente = txtNombreCliente.Text.Trim(),
                IdHabitacion = Convert.ToInt32(cboNumeroHabitacion.SelectedValue),
                Habitacion = txtTipoHabitacion.Text.Trim(),
                IdUsuario = idUsuario,
                FechaInicio = dtpFechaInicioReserva.Value,
                FechaFin = dtpFechaFinReserva.Value,
                PrecioPorNoche = Convert.ToDecimal(txtTipoHabitacion.Tag)
            };

            // Validación de campos vacíos
            if (cboDuiClientes.SelectedValue == null || cboNumeroHabitacion.SelectedValue == null ||
                string.IsNullOrEmpty(nuevaReserva.Cliente) || string.IsNullOrEmpty(nuevaReserva.Habitacion))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpFechaInicioReserva.Value.Date >= dtpFechaFinReserva.Value.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior o igual a la fecha de fin.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Conexion dbHelper = new Conexion();
                dbHelper.InsertarReservaConSP(nuevaReserva);
                MessageBox.Show("Reserva agregada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Abrir el formulario de pagos
                frmNuevoPago frmPago = new frmNuevoPago(
                nuevaReserva.IdReserva = conexion.ObtenerUltimaReservaId(),
                nuevaReserva.Cliente,
                nuevaReserva.Habitacion,
                nuevaReserva.CostoTotal);
                frmPago.ShowDialog(this);

                // Refrescar lista y limpiar campos
                CargarListaNumeroHabitacion();
                DesactivarBotones();
                CargarReservas();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al guardar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarReserva_Click(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow != null)
            {
                // Obtener el Id del usuario seleccionado
                int idReserva = Convert.ToInt32(dgvReservas.CurrentRow.Cells["IdReserva"].Value);

                // Confirmar la eliminación
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta reserva?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    // Eliminar el usuario de la base de datos
                    Reserva reserva = new Reserva();
                    try
                    {
                        conexion.EliminarReservaConSP(idReserva);
                        MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        DesactivarBotones();
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

        private void btnEditarReserva_Click(object sender, EventArgs e)
        {
            /*
            cboDuiClientes.Enabled = true;
            */
            cboNumeroHabitacion.Enabled = true;
            btnCancelarReserva.Enabled = true;
            btnGuardarReserva.Enabled = false;
            btnEditarReserva.Enabled = false;
            btnEliminarReserva.Visible = true;
            
            btnNuevaReserva.Enabled = false;
            btnEditarReserva.Visible = false;
            btnListoEditar.Visible = true;
            btnListoEditar.Enabled = true;

            dtpFechaInicioReserva.Enabled = true;
            dtpFechaFinReserva.Enabled = true;
        }

        private void dgvReservas_SelectionChanged(object sender, EventArgs e)
        {
            // Si no estamos en modo edición, desactivamos los campos
            if (btnEditarReserva.Visible)
            {
                DesactivarBotones();
            }
        }

        private void dgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Hacer clic en una fila válida y en la columna correcta
            {
                DataGridViewRow row = dgvReservas.Rows[e.RowIndex];

                if (row.Cells["Cliente"].Value != null) // Verificar si el valor no es nulo
                {
                    string duiSeleccionado = row.Cells["Dui"].Value.ToString();
                    // Buscar y seleccionar el cliente correspondiente en el ComboBox
                    foreach (DataRowView item in cboDuiClientes.Items)
                    {
                        if (item["nNumeroIdentificacion"].ToString() == duiSeleccionado)
                        {
                            cboDuiClientes.DisplayMember = "nNumeroIdentificacion";
                            cboDuiClientes.ValueMember = "idCliente";
                            cboDuiClientes.SelectedValue = item["idCliente"];
                            break; // Salir del bucle una vez encontrado
                        }
                    }
                    //txtNombreCliente.Text = row.Cells["Cliente"].Value.ToString();

                    string haitacionSeleccionada = row.Cells["Habitacion"].Value.ToString();
                    foreach (DataRowView item in cboNumeroHabitacion.Items)
                    {
                        if (item["numeroHabitacion"].ToString() == haitacionSeleccionada)
                        {
                            cboNumeroHabitacion.DisplayMember = "numeroHabitacion";
                            cboNumeroHabitacion.ValueMember = "IdHabitacion";
                            cboNumeroHabitacion.SelectedValue = item["IdHabitacion"];
                            break; // Salir del bucle una vez encontrado
                        }
                    }
                    //txtTipoHabitacion.Text = row.Cells["tipoHabitación"].Value.ToString();
                    dtpFechaInicioReserva.Value = Convert.ToDateTime(row.Cells["FechaInicio"].Value);
                    dtpFechaFinReserva.Value = Convert.ToDateTime(row.Cells["FechaFin"].Value);

                    
                    btnEditarReserva.Visible = true;
                }
                
            }
        }

        private void btnListoEditar_Click(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow != null)
            {
                // Obtener el Id del usuario seleccionado
                int idReserva = Convert.ToInt32(dgvReservas.CurrentRow.Cells["IdReserva"].Value);

                // Crear un usuario con los datos modificados
                Reserva reservaModificada = new Reserva()
                {
                    IdReserva = idReserva,
                    IdCliente = Convert.ToInt32(cboDuiClientes.SelectedValue),
                    IdHabitacion = Convert.ToInt32(cboNumeroHabitacion.SelectedValue),
                    IdUsuario = idUsuario,
                    FechaInicio = dtpFechaInicioReserva.Value,
                    FechaFin = dtpFechaFinReserva.Value,
                };

                // Validación de campos vacíos
                if (cboDuiClientes.SelectedValue == null || cboNumeroHabitacion.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtpFechaInicioReserva.Value.Date >= dtpFechaFinReserva.Value.Date)
                {
                    MessageBox.Show("La fecha de inicio no puede ser posterior o igual a la fecha de fin.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamar al método para actualizar el usuario en la base de datos
                Conexion dbHelper = new Conexion();
                try
                {
                    dbHelper.ActualizarReservaConSP(reservaModificada);
                    MessageBox.Show("Reserva modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DesactivarBotones();
                    CargarReservas();
                    btnEditarReserva.Enabled = true;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 50002) // Código del error lanzado en caso de nombre de usuario duplicado
                    {
                        MessageBox.Show("La habitación seleccionada no está disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (ex.Number == 50001) // Código del error lanzado si el usuario no existe
                    {
                        MessageBox.Show("El cliente no existe. Por favor, verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar el reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una reserva para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscarReserva_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscarReserva.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(criterio))
            {
                CargarReservas(); // Mostrar todos si no se proporciona criterio de búsqueda
            }
            else
            {
                List<viewReserva> reservas = conexion.ObtenerReservas();

                // Filtrar la lista de reservas basada en el criterio
                var reservaFiltrada = reservas.Where(u =>
                    u.Cliente.ToLower().Contains(criterio) ||
                    u.Dui.ToLower().Contains(criterio) ||
                    u.tipoHabitación.ToLower().Contains(criterio) ||
                    u.Habitacion.ToString().ToLower().Contains(criterio) ||
                    u.precioPorNoche.ToString().ToLower().Contains(criterio) ||
                    u.tipoDivisa.ToString().ToLower().Contains(criterio)).ToList();
            
            
            if (reservaFiltrada.Any())
            {
                // Asignar los resultados filtrados al DataGridView
                dgvReservas.DataSource = reservaFiltrada.ToList();
            }
            else
            {
                MessageBox.Show("No hay resultados con su búsqueda.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBuscarReserva.Text = "";
                CargarReservas();
            }
            dgvReservas.Refresh();
        }
}
    }
}
