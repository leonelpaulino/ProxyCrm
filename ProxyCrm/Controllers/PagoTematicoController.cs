using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyCrm.Models;
namespace ProxyCrm.Controllers
{
    public class PagoTematicoController : ApiController
    {
        // GET: api/PagoTematico/id
        public MensajeRetorno<PagoTematico> Get(string id)
        {
            MensajeRetorno<PagoTematico> returnVal = new MensajeRetorno<PagoTematico>();
            try {
                CrmContext db = new CrmContext();
                returnVal.Data = db.PagoTematico.Consultar(new Guid(id));
                if (returnVal.Data == null)
                {
                    returnVal.State = "FAIL";
                    returnVal.Message = "Este Record no existe.";
                }
                return returnVal;
            }
            catch (Exception e){
                returnVal.State = "FAIL";
                returnVal.Message = e.Message;
                return returnVal;
            }
        }
    }
}
