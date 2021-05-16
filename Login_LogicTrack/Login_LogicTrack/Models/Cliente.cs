using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string movil { get; set; }
        public string cargo { get; set; }
        public int idEmpresa { get; set; }
        public DateTime fechaCreacion { get; set; }
        public Empresa empresa { get; set; }
    }
}