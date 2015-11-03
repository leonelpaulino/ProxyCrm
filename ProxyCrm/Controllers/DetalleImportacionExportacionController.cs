using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyCrm.Models;

namespace ProxyCrm.Controllers
{
    public class DetalleImportacionExportacionController : ApiController
    {
        // GET: api/DetalleImportacionExportacion/5
        public MensajeRetorno<DetalleExportacionImportacion> Get(string id)
        {
            MensajeRetorno<DetalleExportacionImportacion> returnVal = new MensajeRetorno<DetalleExportacionImportacion>();
            try
            {
                CrmContext db = new CrmContext();
                returnVal.Data = db.DetalleExportacionImportacion.Consultar(new Guid(id));
                if (returnVal.Data == null)
                {
                    returnVal.State = "FAIL";
                    returnVal.Message = "Este Record no existe.";
                }
                return returnVal;
            }
            catch (Exception e) {
                returnVal.State = "FAIL";
                returnVal.Message = e.Message;
                return returnVal;
            }

        }
    }
}
