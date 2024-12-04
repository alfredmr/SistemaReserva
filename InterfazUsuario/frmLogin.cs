using frmSistemaReserva.AccesoDatos;
using frmSistemaReserva.InterfazUsuario;
using System;
using System.Windows.Forms;

namespace frmSistemaReserva
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            txtUsuario.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Obtener los datos del formulario
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            // Verificar si el usuario y la contraseña están vacíos
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese tanto el nombre de usuario como la contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                LimpiarCampos();
                return;
            }

            // Crear una instancia de la clase de acceso a datos
            Conexion dbHelper = new Conexion();

            // Verificar si el usuario es válido y obtener su rol
            var result = dbHelper.ValidarUsuarioYObtenerRol(nombreUsuario, contrasena);

            if (result.rol == "Empleado" || result.rol == "Administrador")
            {
                MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el formulario principal y pasar el rol del usuario
                frmPrincipal formPrincipal = new frmPrincipal(result.rol, result.nombreCompleto);
                this.Hide();
                formPrincipal.ShowDialog();
            }
            else if (result.rol == "Usuario bloqueado. Comuníquese con el administrador.")
            {
                MessageBox.Show(result.rol, "Usuario bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarCampos();
                txtUsuario.Focus();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. Por favor, inténtelo nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
                LimpiarCampos();
            }
        }

        private void txtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick(); // Simula un clic en el botón de login
                e.Handled = true; // Marca el evento como manejado (opcional)
                e.SuppressKeyPress = true; // Evita que el Enter se registre en el TextBox
            }
        }


        public void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtContrasena.Clear();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Confirmar si el usuario desea salir de la aplicación
            DialogResult result = MessageBox.Show("¿Está seguro de que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Cerrar la aplicación si el usuario confirma que quiere salir
                Application.Exit();
            }
        }
    }
}
