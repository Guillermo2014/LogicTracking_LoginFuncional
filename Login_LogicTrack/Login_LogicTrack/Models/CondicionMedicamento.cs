using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class CondicionMedicamento
    {
        public int idCondicionMedicamento { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string variableMedicion { get; set; }
        public string unidadMedida { get; set; }
        public double valorMedida { get; set; }
        public double valorMinimo { get; set; }
        public double valorMaximo { get; set; }
        public int idMedicamento { get; set; }
        public Medicamento medicamento { get; set; }
    }
}