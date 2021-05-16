using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class DetalleDespacho
    {
        public int idDetalleDespacho { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string etiqueta { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public double valorProducto { get; set; }
        public double valorTotal { get; set; }
        public int idMedicamento { get; set; }
        public int idDespacho { get; set; }

        public Medicamento medicamento { get; set; }
    }
}