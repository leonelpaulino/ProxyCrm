using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDEspecie
        {
            public Especie Consultar(Guid id) {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    DBEspecie especie = db.DBEspecies.Where(dbes => dbes.amb_especieId == id).FirstOrDefault();
                    Especie ReturnVal = new Especie();
                    if (especie == null)
                        return null;
                    ReturnVal.Id = especie.amb_especieId;
                    ReturnVal.NombreCientifico = especie.amb_nombre_cientifico;
                    ReturnVal.NombreComun = especie.amb_nombre_comun;
                    ReturnVal.Tarifa = especie.new_Tarifa == null? 0 : (double)especie.new_Tarifa;
                    ReturnVal.Tipo = especie.new_Tipo == null? 0: (int)especie.new_Tipo;
                    return ReturnVal;
                }
                catch{
                    return null;
                }

            }
        }
    }
}
