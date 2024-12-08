using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using frmSistemaReserva.Modelos;

namespace frmSistemaReserva.AccesoDatos
{
    //"Server=localhost;Database=SistemaReservas;User Id=sa;Password=Reserva2025;"
    internal class Conexion
    {
        // Cadena de conexión a la base de datos
        private readonly string connectionString = "Server=localhost;Database=SistemaReservas;User Id=sa;Password=Reserva2025;";

        // Método para obtener una nueva conexión a la base de datos
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }

        // Método para abrir una conexión
        public void AbrirConexion(SqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Método para cerrar una conexión
        public void CerrarConexion(SqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // Método para validar el usuario y obtener su rol y tambien el nombre y apellido del usuario.
        public (int idUsuario, string rol, string nombreCompleto) ValidarUsuarioYObtenerRol(string nombreUsuario, string contrasena)
        {
            string mensaje = "Credenciales incorrectas.";
            string rol = "";
            string nombreCompleto = "";
            int idUsuario;

            // Generar el hash de la contraseña
            string hashContrasena = ObtenerHashSHA256(contrasena);

            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    AbrirConexion(connection);

                    // Consulta para verificar si el usuario existe y obtener el rol
                    string query = @"SELECT idUsuario, rolUsuario, usuario_nombre, usuario_apellido, estado
                                    FROM Usuarios
                                    WHERE usuario = @NombreUsuario AND clave = @Contrasena";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@NombreUsuario", System.Data.SqlDbType.NVarChar).Value = nombreUsuario.Trim();
                        command.Parameters.Add("@Contrasena", System.Data.SqlDbType.NVarChar).Value = hashContrasena;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string estado = reader["estado"].ToString();

                                if (estado == "Activo")
                                {
                                    idUsuario = Convert.ToInt32(reader["idUsuario"]);
                                    rol = reader["rolUsuario"].ToString();
                                    string nombre = reader["usuario_nombre"].ToString();
                                    string apellido = reader["usuario_apellido"].ToString();
                                    nombreCompleto = $"{nombre} {apellido}";

                                    return (idUsuario, rol, nombreCompleto);  // Devolver el rol si el usuario es válido y está activo
                                }
                                else if (estado == "Bloqueado")
                                {
                                    return (-1, "Usuario bloqueado. Comuníquese con el administrador.", "");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Usuario no encontrado o credenciales incorrectas.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al validar el usuario: " + ex.Message);
                    return (-1, "Error al validar el usuario.", "");
                }
                finally
                {
                    CerrarConexion(connection);
                }
            }

            return (-1, mensaje, "");
        }


        // Método para obtener el hash de una contraseña usando SHA256
        private string ObtenerHashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        // Método para obtener todos los usuarios
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Idusuario, usuario_nombre, usuario_apellido, correo, usuario, rolUsuario, estado, fechaRegistro FROM Usuarios";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario()
                                {
                                    Id = Convert.ToInt32(reader["Idusuario"]),
                                    Nombre = reader["usuario_nombre"].ToString(),
                                    Apellido = reader["usuario_apellido"].ToString(),
                                    Correo = reader["correo"].ToString(),
                                    NombreUsuario = reader["usuario"].ToString(),
                                    RolUsuario = reader["rolUsuario"].ToString(),
                                    Estado = reader["estado"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(reader["fechaRegistro"])
                                };
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener usuarios: " + ex.Message);
                }
            }

            return usuarios;
        }


        //metodo para insertar usuarios
        public void InsertarUsuarioConSP(Usuario usuario)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("InsertarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@usuario_nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@usuario_apellido", usuario.Apellido);
                        command.Parameters.AddWithValue("@correo", usuario.Correo);
                        command.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
                        command.Parameters.AddWithValue("@clave", ObtenerHashSHA256(usuario.Clave));  // Utilizar la contraseña cifrada
                        command.Parameters.AddWithValue("@rolUsuario", usuario.RolUsuario);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar usuario: " + ex.Message);
                    throw; // Lanzar la excepción nuevamente para que el formulario pueda manejarla
                }
            }
        }

        //metodo para actualizar usuarios
        public void ActualizarUsuarioConSP(Usuario usuario)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("ActualizarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@Idusuario", usuario.Id);
                        command.Parameters.AddWithValue("@usuario_nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@usuario_apellido", usuario.Apellido);
                        command.Parameters.AddWithValue("@correo", usuario.Correo);
                        command.Parameters.AddWithValue("@clave", ObtenerHashSHA256(usuario.Clave));  // Cifrar la clave antes de actualizar
                        command.Parameters.AddWithValue("@rolUsuario", usuario.RolUsuario);
                        command.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);  // Enviar el nombre de usuario para validar duplicados

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar usuario: " + ex.Message);
                    throw; // Relanzar la excepción para manejarla en el formulario
                }
            }
        }

        // Método para eliminar usuarios
        public void EliminarUsuarioConSP(int idUsuario)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("EliminarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar el parámetro para el procedimiento almacenado
                        command.Parameters.AddWithValue("@Idusuario", idUsuario);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar usuario: " + ex.Message);
                    throw; // Relanzar la excepción para manejarla en el formulario
                }
            }
        }
        // Cambio de esado al usuario
        public void CambiarEstadoUsuario(int idUsuario, string nuevoEstado)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado o ejecutar SQL directamente
                    using (SqlCommand command = new SqlCommand("UPDATE Usuarios SET estado = @NuevoEstado WHERE Idusuario = @Idusuario", connection))
                    {
                        command.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                        command.Parameters.AddWithValue("@Idusuario", idUsuario);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cambiar el estado del usuario: " + ex.Message);
                    throw;
                }
            }
        }

        /////////// Modulo reservas
        public void InsertarReservaConSP(Reserva reserva)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("InsertarReserva", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@IdCliente", reserva.IdCliente);
                        command.Parameters.AddWithValue("@IdHabitacion", reserva.IdHabitacion);
                        command.Parameters.AddWithValue("@idusuario", reserva.IdUsuario);
                        command.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
                        command.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar reserva: " + ex.Message);
                    throw; // Lanzar la excepción nuevamente para que el formulario pueda manejarla
                }
            }
        }

        public List<Reserva> ObtenerReservas()
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IdReserva, IdCliente, IdHabitacion, Idusuario, FechaInicio, FechaFin, estado, fechaRegistro FROM Reservas";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Reserva reserva = new Reserva()
                                {

                                    IdReserva = Convert.ToInt32(reader["IdReserva"]),
                                    IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                    IdHabitacion = Convert.ToInt32(reader["IdHabitacion"]),
                                    IdUsuario = Convert.ToInt32(reader["Idusuario"]),
                                    FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                                    FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                                    Estado = reader["estado"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(reader["fechaRegistro"])
                                };
                                reservas.Add(reserva);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener usuarios: " + ex.Message);
                }
            }

            return reservas;
        }

        public DataTable ListarDuiClientes()
        {
            SqlDataReader lista;
            DataTable tabla = new DataTable();
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand comando = new SqlCommand("ObtenerListaDuiClientes", connection);
                    comando.CommandType = CommandType.StoredProcedure;

                    lista = comando.ExecuteReader();
                    tabla.Load(lista);

                    return tabla;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener datos: " + ex.Message);
                    throw ex;
                }

            }
        }

        public string ObtenerNombreporId(int idSeleccionado)
        {
            string nombreCliente = " ";

            // Usar una consulta directa para obtener el nombre del cliente
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand comando = new SqlCommand("SELECT nombreCompleto FROM Clientes WHERE idCliente = @IdCliente", connection);
                    comando.Parameters.AddWithValue("@IdCliente", idSeleccionado);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombreCliente = reader.GetString(reader.GetOrdinal("nombreCompleto"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener nombre: " + ex.Message);
                    throw;
                }
            }

            return nombreCliente;
        }

        public DataTable ListarNumeroHabitaciones()
        {
            SqlDataReader lista;
            DataTable tabla = new DataTable();
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand comando = new SqlCommand("ObtenerNumeroHabitaciones", connection);
                    comando.CommandType = CommandType.StoredProcedure;

                    lista = comando.ExecuteReader();
                    tabla.Load(lista);

                    return tabla;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener datos: " + ex.Message);
                    throw ex;
                }

            }
        }

        public string ObtenerTipoPorId(int idSeleccionado)
        {
            string nombreCliente = " ";

            // Usar una consulta directa para obtener el nombre del cliente
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand comando = new SqlCommand("SELECT tipo FROM Habitaciones WHERE IdHabitacion = @IdHabitacion", connection);
                    comando.Parameters.AddWithValue("@IdHabitacion", idSeleccionado);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombreCliente = reader.GetString(reader.GetOrdinal("tipo"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el tipo: " + ex.Message);
                    throw;
                }
            }

            return nombreCliente;
        }

        public void EliminarReservaConSP(int idReserva)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("EliminarReserva", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar el parámetro para el procedimiento almacenado
                        command.Parameters.AddWithValue("@IdReserva", idReserva);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar reserva: " + ex.Message);
                    throw; // Relanzar la excepción para manejarla en el formulario
                }
            }
        }
    }
}
