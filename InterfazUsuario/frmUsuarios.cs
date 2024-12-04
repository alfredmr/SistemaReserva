using frmSistemaReserva.AccesoDatos;
using frmSistemaReserva.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace frmSistemaReserva.InterfazUsuario
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
            btnBloquear.Visible = false; // Ocultar el botón al iniciar
            txtBuscar.Focus();
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
        }
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            // Actualizar lista de usuarios al cargar el formulario
            ActualizarListaUsuarios();
            LimpiarCampos();
            btnModificar.Enabled = false;
            btnBloquear.Visible = false;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            // Si no estamos en modo edición, desactivamos los campos
            if (!btnEditar.Visible)
            {
                LimpiarCampos();
            }
        }



        private void ActualizarListaUsuarios()
        {
            Conexion dbHelper = new Conexion();
            List<Usuario> usuarios = dbHelper.ObtenerUsuarios();

            // Asignar la lista de usuarios al DataGridView
            dgvUsuarios.AutoGenerateColumns = true;  // Habilitar la generación automática de columnas
            dgvUsuarios.DataSource = usuarios;       // Asignar la lista de usuarios al DataGridView
            dgvUsuarios.Refresh();                   // Refrescar el DataGridView
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Crear un nuevo usuario con los datos ingresados en el formulario
            Usuario nuevoUsuario = new Usuario()
            {
                Nombre = txtUsuarioNombre.Text.Trim(),
                Apellido = txtUsuarioApellido.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                NombreUsuario = txtUsuario.Text.Trim(),
                Clave = txtClave.Text.Trim(),
                RolUsuario = cmbRolUsuario.SelectedItem?.ToString()
            };

            // Validación de campos vacíos
            if (string.IsNullOrEmpty(nuevoUsuario.Nombre) || string.IsNullOrEmpty(nuevoUsuario.Apellido) ||
                string.IsNullOrEmpty(nuevoUsuario.Correo) || string.IsNullOrEmpty(nuevoUsuario.NombreUsuario) ||
                string.IsNullOrEmpty(nuevoUsuario.Clave) || string.IsNullOrEmpty(nuevoUsuario.RolUsuario))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insertar usuario en la base de datos utilizando el procedimiento almacenado
            Conexion dbHelper = new Conexion();
            try
            {
                dbHelper.InsertarUsuarioConSP(nuevoUsuario);
                MessageBox.Show("Usuario agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ActualizarListaUsuarios(); // Refrescar la lista de usuarios
                LimpiarCampos(); // Limpiar y deshabilitar los campos después de agregar
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

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Hacer clic en una fila válida y en la columna correcta
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];

                if (row.Cells["Nombre"].Value != null) // Verificar si el valor no es nulo
                {
                    // Asignar los valores del DataGridView a los TextBoxes y ComboBox
                    txtUsuarioNombre.Text = row.Cells["Nombre"].Value.ToString();
                    txtUsuarioApellido.Text = row.Cells["Apellido"].Value.ToString();
                    txtCorreo.Text = row.Cells["Correo"].Value.ToString();
                    txtUsuario.Text = row.Cells["NombreUsuario"].Value.ToString();
                    cmbRolUsuario.SelectedItem = row.Cells["RolUsuario"].Value.ToString();

                    // Obtener el estado del usuario actual
                    string estadoActual = row.Cells["estado"].Value.ToString();

                    // Cambiar el texto del botón de bloquear/desbloquear según el estado actual
                    if (estadoActual == "Activo")
                    {
                        btnBloquear.Text = "Bloquear";
                    }
                    else if (estadoActual == "Bloqueado")
                    {
                        btnBloquear.Text = "Desbloquear";
                    }
                    
                    // Mostramos el boton editar
                    btnEditar.Visible = true;
                }
            }

        }


        private void LimpiarCampos()
        {
            // Limpiar todos los TextBoxes
            txtUsuarioNombre.Clear();
            txtUsuarioApellido.Clear();
            txtCorreo.Clear();
            txtUsuario.Clear();
            txtClave.Clear();

            // Deseleccionar el ComboBox
            cmbRolUsuario.SelectedIndex = -1;

            // Desactivar todos los campos
            txtUsuarioNombre.Enabled = false;
            txtUsuarioApellido.Enabled = false;
            txtCorreo.Enabled = false;
            txtUsuario.Enabled = false;
            txtClave.Enabled = false;
            cmbRolUsuario.Enabled = false;

            // Configurar botones
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;

            // Ocultar el botón de bloquear/desbloquear y editar
            btnBloquear.Visible = false;
            btnEditar.Visible = false;
        }


        private void activar()
        {
            // Habilitar todos los campos para agregar un nuevo usuario
            txtUsuarioNombre.Enabled = true;
            txtUsuarioApellido.Enabled = true;
            txtCorreo.Enabled = true;
            txtUsuario.Enabled = true; // Nombre de usuario debe ser editable cuando se agrega uno nuevo
            txtClave.Enabled = true;
            cmbRolUsuario.Enabled = true;

            // Configurar botones
            btnAgregar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnNuevo.Enabled = false; // Deshabilitar el botón Nuevo
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                // Obtener el Id del usuario seleccionado
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Id"].Value);

                // Crear un usuario con los datos modificados
                Usuario usuarioModificado = new Usuario()
                {
                    Id = idUsuario,
                    Nombre = txtUsuarioNombre.Text.Trim(),
                    Apellido = txtUsuarioApellido.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    NombreUsuario = txtUsuario.Text.Trim(),  // Esto se enviará, aunque no sea editable
                    Clave = txtClave.Text.Trim(),           // Clave a cifrar antes de actualizar
                    RolUsuario = cmbRolUsuario.SelectedItem?.ToString()
                };

                // Validar campos vacíos
                if (string.IsNullOrEmpty(usuarioModificado.Nombre) || string.IsNullOrEmpty(usuarioModificado.Apellido) ||
                    string.IsNullOrEmpty(usuarioModificado.Correo) || string.IsNullOrEmpty(usuarioModificado.Clave) ||
                    string.IsNullOrEmpty(usuarioModificado.RolUsuario))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamar al método para actualizar el usuario en la base de datos
                Conexion dbHelper = new Conexion();
                try
                {
                    dbHelper.ActualizarUsuarioConSP(usuarioModificado);
                    MessageBox.Show("Usuario modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar y deshabilitar los campos después de modificar
                    LimpiarCampos();
                    btnNuevo.Enabled = true;

                    ActualizarListaUsuarios(); // Refrescar la lista de usuarios
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 50002) // Código del error lanzado en caso de nombre de usuario duplicado
                    {
                        MessageBox.Show("El nombre de usuario ya está en uso. Por favor, intente con un nombre diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (ex.Number == 50001) // Código del error lanzado si el usuario no existe
                    {
                        MessageBox.Show("El usuario no existe. Por favor, verifique e intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos(); // Limpiar todos los campos para evitar residuos de datos previos
            activar();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                // Obtener el Id del usuario seleccionado
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Id"].Value);

                // Confirmar la eliminación
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    // Eliminar el usuario de la base de datos
                    Conexion dbHelper = new Conexion();
                    try
                    {
                        dbHelper.EliminarUsuarioConSP(idUsuario);
                        MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Actualizar la lista de usuarios
                        ActualizarListaUsuarios();

                        // Limpiar los campos y resetear botones
                        LimpiarCampos();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error al eliminar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(criterio))
            {
                ActualizarListaUsuarios(); // Mostrar todos si no se proporciona criterio de búsqueda
            }
            else
            {
                Conexion dbHelper = new Conexion();
                List<Usuario> usuarios = dbHelper.ObtenerUsuarios();

                // Filtrar la lista de usuarios basada en el criterio
                var usuariosFiltrados = usuarios.Where(u =>
                    u.Nombre.ToLower().Contains(criterio) ||
                    u.Apellido.ToLower().Contains(criterio) ||
                    u.Correo.ToLower().Contains(criterio)).ToList();

                // Asignar los resultados filtrados al DataGridView
                dgvUsuarios.DataSource = usuariosFiltrados;
                dgvUsuarios.Refresh();
            }
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                // Obtener el Id del usuario seleccionado
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Id"].Value);
                string estadoActual = dgvUsuarios.CurrentRow.Cells["estado"].Value.ToString();

                Conexion dbHelper = new Conexion();
                try
                {
                    // Cambiar el estado del usuario
                    string nuevoEstado = estadoActual == "Activo" ? "Bloqueado" : "Activo";
                    dbHelper.CambiarEstadoUsuario(idUsuario, nuevoEstado);

                    // Mostrar mensaje de confirmación
                    MessageBox.Show($"Estado de usuario actualizado exitosamente a {nuevoEstado}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizar la lista de usuarios para reflejar los cambios
                    ActualizarListaUsuarios();

                    // Cambiar el texto del botón de acuerdo con el nuevo estado
                    btnBloquear.Text = nuevoEstado == "Activo" ? "Bloquear" : "Desbloquear";

                    // Limpiar y restablecer el formulario
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el estado del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para bloquear/desbloquear.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            activar();
            btnEditar.Visible = false;
            btnAgregar.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBloquear.Visible = true;
        }
    }
}
