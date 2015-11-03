using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCrm.Models
{
    public class RecursosForestales
    {
        public Guid Id { get; set; }
        public Solicitante Solicitante { get; set; }
        public string NumeroSolicitud { get; set; }
        public double MontoUSD { get; set; }
        public string Observaciones { get; set; }
        public string Puerto { get; set; }
        public string Barco { get; set; }
        public string Suplidor { get; set; }
        public string PaisProcedencia { get; set; }

        public double Cantidad { get; set; }
        public double PiesTablar { get; set; }
        public int Concepto { get; set; }
        public DetalleExportacionImportacion Madera {get;set;}
    }
}
