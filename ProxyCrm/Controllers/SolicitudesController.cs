using ProxyCrm.Filters;
using ProxyCrm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProxyCrm.Controllers
{
    [AuthenticationFilter]
    public class SolicitudesController : ApiController
    {   
        /// <summary>
        /// Retorna una lista de solicitud de un usuario.
        /// </summary>
        /// <param name="id">id del usuairo</param>
        /// <returns>Retorna una lista de solicitud</returns>
        [HttpGet]
        // GET: api/Solicitudes/Collection/1
        public MensajeRetorno <IEnumerable<Solicitudes>> Collection(string id)
        {
            CrmContext db = new CrmContext();
            Guid guid = new Guid();
            if (!Guid.TryParse(id, out guid))
                return new MensajeRetorno<IEnumerable<Solicitudes>> { Data = null, Message = "Este id no es valido", State = "FAIL" };
            var result = db.SolicitudesEnLinea.ConsultarTodo(guid);
            if ( result == null)
                return new MensajeRetorno<IEnumerable<Solicitudes>> { Data = null, Message = "Este usuario no tiene solicitudes", State = "SUCCESS" };
            return new MensajeRetorno<IEnumerable<Solicitudes>> { Data = result, Message = "La solicitud no se pudo crear", State = "SUCCESS" };
        }

        /// <summary>
        /// Retorna una solicitud dado su id.
        /// </summary>
        /// <param name="id">id de la solicitud</param>
        /// <returns>Solicitud.</returns>
        [HttpGet]
        // GET: api/Solicitudes/Details/5
        public MensajeRetorno<Solicitudes> Details(string id)
        {
            
            CrmContext db = new CrmContext();
            Guid guid = new Guid();
            if (!Guid.TryParse(id, out guid))
                return new MensajeRetorno<Solicitudes> { Data = null, Message = "Este id no es valido", State = "FAIL" };
            var result = db.SolicitudesEnLinea.Consultar(guid);
            if (result == null)
                return new MensajeRetorno<Solicitudes> { Data = null, Message = "La solicitud no se pudo crear", State = "SUCCESS" };
            return new MensajeRetorno<Solicitudes> { Data = result,Message = "", State= "SUCCESS" };
        }
        /// <summary>
        /// Inserta una solicitud en el crm.
        /// </summary>
        /// <param name="solicitud">Solicitud.</param>
        /// <returns>Retorna la solicitud insertada</returns>
        [HttpPost]
        // PUT: api/SolicitudEnLinea/5
        public MensajeRetorno<Solicitudes> Create(Solicitudes solicitud)
        {
            CrmContext db = new CrmContext();
            var result = db.SolicitudesEnLinea.Crear(solicitud);
            if (result == null)
                return new MensajeRetorno<Solicitudes> { Data = null, Message = "No se pudo crear la solicitud.Intente más tarde.",State ="FAIL"};
            solicitud.Id = result;
            return new MensajeRetorno<Solicitudes> { Data = solicitud, Message = null, State = "SUCCESS" };
        }
    }
}
