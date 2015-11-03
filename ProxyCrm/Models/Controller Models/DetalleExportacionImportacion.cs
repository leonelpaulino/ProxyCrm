using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    public class DetalleExportacionImportacion
    {
        public Guid Id { get; set; }
        public Guid PagoTematico { get; set; }
        public double M3 { get; set; }
        public double PiesTablar {get;set;}
        public Especie Especie { get; set; }
    }
}