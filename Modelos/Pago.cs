using System;

namespace frmSistemaReserva.Modelos
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdReserva { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
        public string TipoDivisa { get; set; }
    }
}
