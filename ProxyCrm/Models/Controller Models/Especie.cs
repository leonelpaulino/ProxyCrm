using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    public class Especie
    {
        public Guid Id { get; set; }
        public string NombreComun { get; set; }
        public string NombreCientifico {get;set;}

        public int Tipo { get; set; }
        public double Tarifa { get; set; }

    }
}