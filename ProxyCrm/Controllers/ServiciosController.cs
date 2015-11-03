using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyCrm.Models;
using ProxyCrm.Filters;
namespace ProxyCrm.Controllers
{
    [AuthenticationFilter]
    public class ServiciosController : ApiController
    {
        /// <summary>
        /// Retorna la lista de todos los servicios.
        /// GET: api/Servicios/collection
        /// </summary>
        /// <returns> Retorna la lista de todos los servicios</returns>

        [HttpGet]
        public MensajeRetorno<IEnumerable<Servicio>> collection()
        {
            CrmContext db = new CrmContext();
            var result = db.Servicios.ConsultarTodos();
            if (result == null)
                return new MensajeRetorno <IEnumerable<Servicio>> { Data = null, Message = "No existe ningún servicio", State = "SUCCESS" };
            
            return new MensajeRetorno<IEnumerable<Servicio>> { Data = result, Message ="", State = "SUCCESS"};
        }

        /// <summary>
        /// Retorna un servicio dado su id.
        /// GET: api/Servicios/5
        /// </summary>
        /// <param name="id">Id del servicio</param>
        /// <returns>Retorna el servicio.</returns>
        [HttpGet]
        public MensajeRetorno<Servicio> Get(string id)
        {
            CrmContext db = new CrmContext();
            Guid guid = new Guid();
            if (!Guid.TryParse(id, out guid))
                return new MensajeRetorno<Servicio> { Data = null, Message = "Este id no es valido.", State = "FAIL" };
            var result = db.Servicios.Consultar(guid);
            if (result == null)
                return new MensajeRetorno<Servicio> { Data = null, Message = "No se encontro ningún estado con este id.", State = "FAIL" };
            return new MensajeRetorno<Servicio> { Data = result, Message = "", State = "SUCCESS" };
        }
    }
}
