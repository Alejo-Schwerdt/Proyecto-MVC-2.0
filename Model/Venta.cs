using System;
using System.Collections.Generic;

namespace Model
{
    public class Venta
    {
        public int IDVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }

        // Lista de detalles
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
