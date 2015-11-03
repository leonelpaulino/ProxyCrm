using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDServicio
        {
            /// <summary>
            /// Retorna todos los servicios.
            /// </summary>
            /// <returns></returns>
            public List<Servicio> ConsultarTodos()
            {
                CRMDataContext db = new CRMDataContext();

                var list = db.DBServicios;
                if (list.Count() == 0)
                    return null;
                List<Servicio> Retu = new List<Servicio>();
                foreach (DBServicios i in list)
                {
                    Retu.Add(new Servicio { Id = i.new_servicioId, NombreServicios = i.new_servicio, Descripcion = i.new_descripcion});
                }
                return Retu;
            }
            /// <summary>
            /// Retorna un servicio dado el guid
            /// </summary>
            /// <returns> Retorna un servicio</returns>
            public Servicio Consultar(Guid servicioid)
            {
                CRMDataContext db = new CRMDataContext();

                var result = db.DBServicios.Where(servicio => servicio.new_servicioId== servicioid).SingleOrDefault();
                if (result == null)
                    return null;
                Servicio returnVal = new Servicio { Id = result.new_servicioId, NombreServicios = result.new_servicio, Descripcion = result.new_descripcion};
                return returnVal;
            }
        }
    }
}