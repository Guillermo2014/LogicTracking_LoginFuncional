using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class Empresa
    {
        public int idEmpresaCliente { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string razonSocial { get; set; }
        public string identificacion { get; set; }
        public string departamento { get; set; }
        public string ciudad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
    }
}