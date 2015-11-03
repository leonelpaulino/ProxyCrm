using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    public class Archivo
    {
        public Guid ArchivoId { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoDeArchivo { get; set; }
        public byte[] Contenido { get; set; }
        public int Longitud { get; set; }

    }
}