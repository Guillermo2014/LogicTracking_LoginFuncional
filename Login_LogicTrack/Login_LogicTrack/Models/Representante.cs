using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class Representante
    {
        public int idRepresentante { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public int telefonoMovil { get; set; }
        public string cargo { get; set; }
    }
}