using ProxyCrm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProxyCrm.Controllers
{
    public class RecursosForestalesController : ApiController
    {
        public MensajeRetorno<RecursosForestales> Get(string id)
        {
            MensajeRetorno<RecursosForestales> returnVal = new MensajeRetorno<RecursosForestales>();
            try
            {
                CrmContext db = new CrmContext();
                returnVal.Data = db.RecursosForestales.Consultar(new Guid(id));
                if (returnVal.Data == null)
                {
                    returnVal.State = "FAIL";
                    returnVal.Message = "Este Record no existe.";
                }
                return returnVal;
            }
            catch (Exception e)
            {
                returnVal.State = "FAIL";
                returnVal.Message = e.Message;
                return returnVal;
            }

        }

        // POST: api/DetalleImportacionExportacion
        public MensajeRetorno<RecursosForestales> Post(RecursosForestales record)
        {
            MensajeRetorno<RecursosForestales> returnVal = new MensajeRetorno<RecursosForestales>();
            try
            {
                CrmContext db = new CrmContext();
                Nullable<Guid> id = db.RecursosForestales.Crear(record);
                if (id == null)
                {
                    returnVal.State = "FAIL";
                    returnVal.Message = "No se pudo crear el record.Porfavor contactese con el Ministerio de Medio Ambiente";
                }
                returnVal.Data = db.RecursosForestales.Consultar((Guid)id);
                return returnVal;
            }
            catch (Exception e)
            {
                returnVal.State = "FAIL";
                returnVal.Message = e.Message;
                return returnVal;
            }
        }

        // PUT: api/DetalleImportacionExportacion/5
        public MensajeRetorno<RecursosForestales> Put(string id, RecursosForestales record)
        {
            MensajeRetorno<RecursosForestales> returnVal = new MensajeRetorno<RecursosForestales>();
            try
            {
                CrmContext db = new CrmContext();
                record.Id = new Guid(id);
                Nullable<Guid> resultid = db.RecursosForestales.Update(record);
                if (id == null)
                {
                    returnVal.State = "FAIL";
                    returnVal.Message = "No se pudo Actualizar el record.Porfavor contactese con el Ministerio de Medio Ambiente";
                }
                returnVal.Data = db.RecursosForestales.Consultar((Guid)resultid);
                return returnVal;
            }
            catch (Exception e)
            {
                returnVal.State = "FAIL";
                returnVal.Message = e.Message;
                return returnVal;
            }

        }
    }
}
