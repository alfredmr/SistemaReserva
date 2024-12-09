using System;

namespace frmSistemaReserva.Modelos
{
    internal class Reserva
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdHabitacion { get; set; }
        public string Habitacion { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Propiedad calculada: número de días
        public int NumeroDias
        {
            get
            {
                return (FechaFin - FechaInicio).Days;
            }
        }

        // Propiedad calculada: costo total
        public decimal CostoTotal
        {
            get
            {
                return NumeroDias * PrecioPorNoche;
            }
        }

    }
    internal class viewReserva
    {
        public int IdReserva { get; set; }
        public string Cliente { get; set; }
        public string Dui { get; set; }
        public int Habitacion { get; set; }
        public string tipoHabitación { get; set; }
        public string precioPorNoche { get; set; }
        public string usuarioResponsable { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string estadoReserva { get; set; }
        public string estadoHabitacion { get; set; }
        public string estadoPago { get; set; }
        public string montoPagado { get; set; }
        public DateTime fechaPagado { get; set; }
        public string metodoPaga { get; set; }
        public string tipoDivisa { get; set; }
        public DateTime fechaReserva { get; set; }

    }
    internal class ListItem
    {
        public string Text { get; set; }
        public string Estado { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }


}
