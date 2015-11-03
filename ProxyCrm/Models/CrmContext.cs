using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public CrmContext() {
            Archivos = new CRUDArchivo();
            Estados = new CRUDEstado();
            Servicios = new CRUDServicio();
            Solicitantes = new CRUDSolicitante();
            SolicitudesEnLinea = new CRUDSolicitudes();
            Especies = new CRUDEspecie();
            PagoTematico = new CRUDPagoTematico();
            RecursosForestales = new CRUDRecursosForestales();
            DetalleExportacionImportacion = new CRUDDetalleExportacionImportacion();

        }
        public CRUDArchivo Archivos { get; set; }
        public CRUDEstado Estados { get;}
        public CRUDServicio Servicios { get; set; }

        public CRUDSolicitante Solicitantes { get; set; }

        public CRUDSolicitudes SolicitudesEnLinea { get; set; }
        public CRUDDetalleExportacionImportacion DetalleExportacionImportacion { get; set; }
        public CRUDRecursosForestales RecursosForestales { get; set; }
        public CRUDPagoTematico PagoTematico { get; set; }
        public CRUDEspecie Especies { get; set; }

    }
}