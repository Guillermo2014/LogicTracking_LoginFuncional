using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class Medicamento
    {
        public int IdMedicamento { get; set; }
        public string nombreMedicamento { get; set; }
        public DateTime fechaRecepcion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string lote { get; set; }
        public int cantidad { get; set; }
        public int idProveedor { get; set; }
        public Proveedor proveedor { get; set; }
        //public int representante { get; set; }
        public List<CondicionMedicamento> condicionesMedicamento { get; set; }
    }
}