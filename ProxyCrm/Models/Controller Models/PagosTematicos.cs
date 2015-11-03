using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    public class PagoTematico
    {
        public Guid Id { get; set; }
        public Solicitante Solicitante { get; set; }
        public string Cheque { get; set; }
        public int IdAreaTematica { get; set; }
        public int AreaTematica { get; set; }
        public int ConceptoRF { get; set; }
        // AreaTematica Recursos Forestales
        public DetalleExportacionImportacion Detalle { get;set; }
        public RecursosForestales Recursos { get; set; }
        public double PiesTablar { get; set; }
        public double M3 { get; set; }
        public double MontoRd { get; set; }
        public string Observaciones { get; set; }
        public string CodigoTarifa { get; set; }
    }
}