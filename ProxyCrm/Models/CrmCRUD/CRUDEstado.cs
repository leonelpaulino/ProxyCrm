using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ProxyCrm.Models
{
    partial class CrmContext
    {

        public class CRUDEstado
        {
            /// <summary>
            /// Retorna Todos los estados dado un servicio
            /// </summary>
            /// <returns></returns>
            public List<Estado> ConsultarTodo(Guid servicioid)
            {
                CRMDataContext db = new CRMDataContext();
                
                var list = db.DBServicioEstados.Where(est=> est.new_servicioid == servicioid);
                List<DBEstados> dblist = new List<DBEstados>();
                foreach (DBServicioEstados i in list) {

                    var element = db.DBEstados.Where(DBEstados => DBEstados.new_estadosId == i.new_estadosid).SingleOrDefault();
                    if (element == null)
                        continue;
                    dblist.Add(element);
                }
                if (list.Count() == 0)
                    return null; 
                List<Estado> returnVal = new List<Estado>();
                foreach (DBEstados i in dblist)
                {
                    returnVal.Add(new Estado { Id = i.new_estadosId, EstadoActual = i.new_estado, Posicion = i.new_Estado_Id.Value });
                }
                return returnVal;
            }

            /// <summary>
            /// Retorna un estado dado el guid
            /// </summary>
            /// <returns> Retorna un estado</returns>
            public Estado Consultar(Guid estadoid)
            {
                CRMDataContext db = new CRMDataContext();

                var result = db.DBEstados.Where(Estado => Estado.new_estadosId == estadoid ).SingleOrDefault();
                if (result == null)
                    return null;
                Estado returnVal = new Estado { Id = result.new_estadosId, EstadoActual = result.new_estado,Posicion = result.new_Estado_Id.Value };
                return returnVal;
            }
        }
    }
}