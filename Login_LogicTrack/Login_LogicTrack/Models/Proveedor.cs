using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_LogicTrack.Models
{
    public class Proveedor
    {
        public int idProveedor { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string razonSocial { get; set; }
        public Representante representante { get; set; }
        public string identificacion { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }
        public string direccion { get; set; }
        public int idRepresentante { get; set; }
    }
}