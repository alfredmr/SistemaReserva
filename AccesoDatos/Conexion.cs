﻿using System;
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

        // Método para insertar las raservas
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
                    Console.WriteLine("Error al gaurdar reserva: " + ex.Message);
                    throw; // Lanzar la excepción nuevamente para que el formulario pueda manejarla
                }
            }
        }

        // Método para visualizar reservas
        public List<viewReserva> ObtenerReservas()
        {
            List<viewReserva> viewReservas = new List<viewReserva>();

            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    string query = "select * from dbo.vw_ReservasDetalle";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                viewReserva reserva = new viewReserva()
                                {
                                    IdReserva = Convert.ToInt32(reader["IdReserva"]),
                                    Cliente = reader["Cliente"].ToString(),
                                    Dui = reader["IdentificacionCliente"].ToString(),
                                    Habitacion = Convert.ToInt32(reader["NumeroHabitacion"]),
                                    tipoHabitación = reader["TipoHabitacion"].ToString(),
                                    precioPorNoche = reader["PrecioPorNoche"].ToString(),
                                    usuarioResponsable = reader["UsuarioResponsable"].ToString(),
                                    fechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                                    fechaFin = Convert.ToDateTime(reader["FechaFin"]),
                                    estadoReserva = reader["EstadoReserva"].ToString(),
                                    estadoHabitacion = reader["EstadoHabitacion"].ToString(),
                                    estadoPago = reader["EstadoPago"].ToString(),
                                    montoPagado = reader["MontoPagado"]?.ToString(),
                                    fechaPagado = reader["FechaPago"] != DBNull.Value ? Convert.ToDateTime(reader["FechaPago"]) : DateTime.MinValue,
                                    metodoPaga = reader["MetodoPago"]?.ToString(),
                                    tipoDivisa = reader["TipoDivisa"]?.ToString(),
                                    fechaReserva = Convert.ToDateTime(reader["FechaReserva"])
                                };
                                viewReservas.Add(reserva);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener reservas: " + ex.Message);
                }
            }

            return viewReservas;
        }

        // Método para obtener los DUI de los clientes
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

        public void ActualizarReservaConSP(Reserva reserva)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("ModificarReserva", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
                        command.Parameters.AddWithValue("@IdCliente", reserva.IdCliente);
                        command.Parameters.AddWithValue("@IdHabitacion", reserva.IdHabitacion);
                        command.Parameters.AddWithValue("@idusuario", reserva.IdUsuario);
                        command.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
                        command.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Error de base de datos al actualizar reserva: " + sqlEx.Message);
                    throw; // Relanzar la excepción para manejarla en el formulario
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar reserva: " + ex.Message);
                    throw; // Relanzar la excepción para manejarla en el formulario
                }
            }
        }

        public int ObtenerUltimaReservaId()
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MAX(IdReserva) FROM Reservas";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la última reserva: " + ex.Message);
                    throw;
                }
            }
        }


        public void InsertarPagoConSP(Pago pago)
        {
            using (SqlConnection connection = ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Crear el comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("InsertarPago", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@IdReserva", pago.IdReserva);
                        command.Parameters.AddWithValue("@Monto", pago.Monto);
                        command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                        command.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);
                        command.Parameters.AddWithValue("@TipoDivisa", pago.TipoDivisa);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al registrar el pago: " + ex.Message);
                    throw; // Relanzar la excepción para manejarla en el formulario
                }
            }
        }

    }
}
