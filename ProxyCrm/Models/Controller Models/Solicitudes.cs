using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCrm.Models
{
    public class Solicitudes
    {
        public Solicitante Solicitantes
        { get; set; }
        public string Nota {get;set;}
        public Servicio Servicios
        { get; set; }
        public string Institucion
        { get; set; }
        public string Direccion
        { get; set; }
        public string Telefono
        { get; set; }
        public string NoSolicitud { get; set; }
        public string CorreoElectronico
        { get; set; }

        public int CantParticipante
        { get; set; }
        public DateTime Fecha { get; set; }
        public virtual List<Archivo> Archivos
        { get; set; }
        public Estado EstadoActual { get; set; }
        public Guid Id { get; set; }
        
    }
}
