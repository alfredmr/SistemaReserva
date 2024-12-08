using System;

namespace frmSistemaReserva.Modelos
{
    internal class Reserva
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdHabitacion { get; set; }
        public int IdUsuario { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
