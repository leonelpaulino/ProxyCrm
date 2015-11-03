using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    public class Solicitante
    {
        public string NombreCompleto { get; set; }
        public string Clave { get; set; }
        public string Cedula { get; set; }
        public string ConfirmarClave { get; set; }
        public string CorreoElectronico{ get; set; }
        public string Telefono{get; set;}
        public Guid Id { get; set; }
        

    }
}