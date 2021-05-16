using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class LogMedicamento
    {
        public int idLogMedicion { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string sensor { get; set; }
        public double valorMedicion { get; set; }
        public double valorMaximo { get; set; }
        public double valorMinimo { get; set; }
        public int idDespacho { get; set; }

    }
}