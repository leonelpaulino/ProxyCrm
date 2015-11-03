using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyCrm.Filters;
using ProxyCrm.Models;

namespace ProxyCrm.Controllers
{
    [AuthenticationFilter]
    public class EstadosController : ApiController
    {
        /// <summary>
        /// Retorna todos los posible estados de un servicio.
        /// GET: /api/Estados/collection/1
        /// </summary>
        /// <param name="id">Id del servicio</param>
        /// <returns>Retorna una lista de estados.</returns>
        [HttpGet]
        public MensajeRetorno<IEnumerable<Estado>> collection(string id)
        {
            CrmContext db = new CrmContext();
            Guid guid = new Guid();
            if (!Guid.TryParse(id, out guid))
                return new MensajeRetorno<IEnumerable<Estado>> { Data = null, Message = "Este id no es valido", State = "FAIL" };
            var result = db.Estados.ConsultarTodo(guid);
            if ( result == null) 
                return new MensajeRetorno<IEnumerable<Estado>> { Data= null, Message = "Este servicio no tiene estados.",State = "FAIL"};
            return new MensajeRetorno<IEnumerable<Estado>> { Data = result, Message = "", State = "SUCCESS" };
        }
        /// <summary>
        /// Retorna un estado dado su id.
        // GET: api/Estados/5
        /// </summary>
        /// <param name="id">Id del estado</param>
        /// <returns>Retorna el estado encontrado.</returns>
        [HttpGet]
        
        public MensajeRetorno<Estado> Get(string id)
        {
            CrmContext db = new CrmContext();
            Guid guid = new Guid();
            if ( !Guid.TryParse(id,out guid) )
                return new MensajeRetorno<Estado> { Data = null, Message = "No existe ningún estado con este id.", State = "FAIL" };
            var result = db.Estados.Consultar(guid);
            if (result == null)
                return new MensajeRetorno<Estado> { Data = null, Message = "No existe ningún estado con este id.", State = "FAIL" };
            return new MensajeRetorno<Estado> { Data = result, Message = "", State = "SUCCESS" };
        }
    }
}
