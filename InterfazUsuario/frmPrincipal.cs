using frmSistemaReserva.Modelos;
using System;
using System.Windows.Forms;
namespace frmSistemaReserva.InterfazUsuario
{
    public partial class frmPrincipal : Form
    {
        private string rolUsuario;
        private string usuarioActual;
        private int idUsuario;
        public frmPrincipal(int idUsuario, string rol, string nombreCompleto)
        {
            InitializeComponent();
            this.rolUsuario = rol;
            this.usuarioActual = nombreCompleto;
            this.idUsuario = idUsuario;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = usuarioActual;

            if (rolUsuario != "Administrador")
            {
                btnUsuarios.Enabled = false;   // Deshabilitar el botón de usuarios
                lblUsuarios.Enabled = false;   // Deshabilitar el label de usuarios
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // Abrir el formulario de Usuarios
            frmUsuarios formUsuarios = new frmUsuarios();
            formUsuarios.ShowDialog(this);
        }

        private void btnSingOut_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario principal y abrir el formulario de login
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
            this.Close();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            frmReservas formReservas = new frmReservas(idUsuario);
            formReservas.ShowDialog(this);
        }
    }
}
