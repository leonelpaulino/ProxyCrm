using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDDetalleExportacionImportacion
        {
            /// <summary>
            /// Consulta un detalle por el id de un record de recursos forestales.
            /// </summary>
            /// <param name="id">id del record de recursos forestales</param>
            /// <returns>Retorna el detalle o null si no encuentra nada.</returns>
            public DetalleExportacionImportacion ConsultarPagoTematico(Guid id)
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    CrmContext db2 = new CrmContext();
                    var detalle = db.DBDetalleExportacionImportacions.Where(dbdetalle => dbdetalle.new_Pago_tematico == id).FirstOrDefault();
                    if (detalle == null)
                        return null;
                    DetalleExportacionImportacion returnVal = Asignar(detalle);
                    return returnVal;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Consulta un detalle por el id de un record de recursos forestales.
            /// </summary>
            /// <param name="id">id del record de recursos forestales</param>
            /// <returns>Retorna el detalle o null si no encuentra nada.</returns>
            public DetalleExportacionImportacion ConsultarRecursosForestales(Guid id)
            {
                try{
                    CRMDataContext db = new CRMDataContext();
                    CrmContext db2 = new CrmContext();
                    var detalle = db.DBDetalleExportacionImportacions.Where(dbdetalle => dbdetalle.new_RecursosForestales == id).FirstOrDefault();
                    if (detalle == null)
                        return null;
                    DetalleExportacionImportacion returnVal = Asignar(detalle);
                    return returnVal;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public DetalleExportacionImportacion Consultar(Guid id)
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    CrmContext db2 = new CrmContext();
                    var detalle = db.DBDetalleExportacionImportacions.Where(dbdetalle => dbdetalle.new_detalleexportacionimportacionId == id).FirstOrDefault();
                    if (detalle == null)
                        return null;
                    DetalleExportacionImportacion returnVal = Asignar(detalle);
                    return returnVal;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="detalle"></param>
            /// <returns></returns>
            private DetalleExportacionImportacion Asignar(DBDetalleExportacionImportacion detalle)
            {
                CrmContext db2 = new CrmContext();
                DetalleExportacionImportacion returnVal = new DetalleExportacionImportacion();
                returnVal.Especie = db2.Especies.Consultar((Guid)detalle.new_EspecieLookup);
                returnVal.Id = detalle.new_detalleexportacionimportacionId;
                returnVal.M3 = detalle.new_M3 == null? 0: (double)detalle.new_M3;
                returnVal.PiesTablar = detalle.new_Piestablar == null ? 0 : (double)detalle.new_Piestablar;
                return returnVal;

            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="record"></param>
            /// <returns></returns>
            public Guid Crear(DetalleExportacionImportacion record)
            {
                try
                {
                    CrmProxy db = new CrmProxy();
                    Entity newRow = new Entity("new_detalleexportacionimportacion");
                    newRow["new_especielookup"] = new EntityReference("amb_especie", record.Especie.Id);
                    newRow["new_m3"] = record.M3;
                    newRow["new_piestablar"] = record.PiesTablar;
                    newRow["x.new_Pago_tematico"] = record.PagoTematico;
                    return db.Service.Create(newRow);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}