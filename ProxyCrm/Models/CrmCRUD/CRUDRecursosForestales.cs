using Microsoft.Xrm.Sdk;
using ProxyCrm.Models.CrmCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDRecursosForestales
        {
            private Guid CrearBase() {
                try {
                    CRMDataContext db = new CRMDataContext();
                    amb_viceministerio_recursos_forestalesBase baserecord = new amb_viceministerio_recursos_forestalesBase();
                    baserecord.amb_viceministerio_recursos_forestalesId = Guid.NewGuid();
                    baserecord.CreatedOn = DateTime.Now;
                    baserecord.CreatedBy = BaseConfiguration.userId;
                    baserecord.OwnerId = BaseConfiguration.userId;
                    baserecord.OwningBusinessUnit = BaseConfiguration.OwningUnit;
                    baserecord.statecode = 1;
                    baserecord.OwnerIdType = 8;
                    baserecord.ModifiedOn = DateTime.Now;
                    baserecord.ModifiedBy = BaseConfiguration.userId;
                    db.amb_viceministerio_recursos_forestalesBases.InsertOnSubmit(baserecord);
                    db.SubmitChanges();
                    return baserecord.amb_viceministerio_recursos_forestalesId;
                }
                catch(Exception e)
                {
                    throw e;
                }
                
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="record"></param>
            /// <returns></returns>
            public Nullable<Guid> Crear(RecursosForestales record)
            {

                try
                {
                    CrmProxy db = new CrmProxy();
                    Entity newRecord = new Entity("");
                    //CRMDataContext db = new CRMDataContext();
                    //CrmContext db2 = new CrmContext();
                    //DBViceministerioRecursosForestales recursosforestales = new DBViceministerioRecursosForestales();
                    //recursosforestales.amb_viceministerio_recursos_forestalesId = CrearBase();
                    //recursosforestales.amb_telefono = record.Solicitante.Telefono;
                    //recursosforestales.amb_pies_tablar = record.PiesTablar;
                    //recursosforestales.new_Puerto = record.Puerto;
                    //recursosforestales.new_Barco_Vapor = record.Barco;
                    //recursosforestales.new_Suplidor = record.Suplidor;
                    //recursosforestales.new_Pais_de_Procedencia = record.PaisProcedencia;
                    //recursosforestales.amb_cantidad = record.Cantidad;
                    //var solicitante = db2.Solicitantes.ConsultarTodos(record.Solicitante.Cedula);
                    //recursosforestales.amb_No_RNCId = solicitante.Id;
                    //recursosforestales.new_Concepto_RF = record.Concepto;
                    //recursosforestales.amb_solicitante = solicitante.NombreCompleto;
                    //db.DBViceministerioRecursosForestales.InsertOnSubmit(recursosforestales);
                    //db.SubmitChanges();
                    //return recursosforestales.amb_viceministerio_recursos_forestalesId;


                }
                catch (Exception e)
                {
                    throw e;
                }


            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="record"></param>
            /// <returns></returns>
            public Nullable<Guid> Update(RecursosForestales record)
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    CrmContext db2 = new CrmContext();
                    var recursosforestales = db.DBViceministerioRecursosForestales.Where(recu => recu.amb_viceministerio_recursos_forestalesId == record.Id).SingleOrDefault();
                    if (recursosforestales == null)
                        return null;
                    recursosforestales.amb_telefono = record.Solicitante.Telefono;
                    recursosforestales.amb_pies_tablar = record.PiesTablar;
                    recursosforestales.new_Puerto = record.Puerto;
                    recursosforestales.new_Barco_Vapor = record.Barco;
                    recursosforestales.new_Suplidor = record.Suplidor;
                    recursosforestales.new_Pais_de_Procedencia = record.PaisProcedencia;
                    recursosforestales.amb_cantidad = record.Cantidad;
                    var solicitante = db2.Solicitantes.ConsultarTodos(record.Solicitante.Cedula);
                    recursosforestales.amb_No_RNCId = solicitante.Id;
                    recursosforestales.new_Concepto_RF = record.Concepto;
                    recursosforestales.amb_solicitante = solicitante.NombreCompleto;
                    db.SubmitChanges();
                    return recursosforestales.amb_viceministerio_recursos_forestalesId;
                }

                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="recursos"></param>
            /// <returns></returns>
            internal bool Existe(RecursosForestales recursos)
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    if (db.DBViceministerioRecursosForestales.Any(recu => recu.amb_viceministerio_recursos_forestalesId == recursos.Id))
                        return true;
                    else
                        return false;
                }
                catch (Exception e){
                    throw e; 
                }
               
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public RecursosForestales Consultar(Guid id)
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    CrmContext db2 = new CrmContext();
                    var recursos = db.DBViceministerioRecursosForestales.Where(recu => recu.amb_viceministerio_recursos_forestalesId == id).SingleOrDefault();
                    if (recursos == null)
                        return null;
                    RecursosForestales returnVal = new RecursosForestales();
                    returnVal.Barco = recursos.new_Barco_Vapor == null ? "" : recursos.new_Barco_Vapor;
                    returnVal.Cantidad = recursos.amb_cantidad == null ? 0 : (double)recursos.amb_cantidad;
                    returnVal.Concepto = recursos.new_Concepto_RF == null ? 0 : (int)recursos.new_Concepto_RF;
                    returnVal.Id = recursos.amb_viceministerio_recursos_forestalesId;
                    returnVal.MontoUSD = recursos.new_Monto_USD == null ? 0 : (double)recursos.new_Monto_USD;
                    returnVal.NumeroSolicitud = recursos.new_Numero_Solicitud;
                    returnVal.Observaciones = recursos.amb_Observaciones;
                    returnVal.PaisProcedencia = recursos.new_Pais_de_Procedencia;
                    returnVal.Puerto = recursos.new_Puerto;
                    returnVal.Suplidor = recursos.new_Suplidor;
                    returnVal.PiesTablar = recursos.amb_pies_tablar == null ? 0 : (double)recursos.amb_pies_tablar;
                    returnVal.Madera = db2.DetalleExportacionImportacion.ConsultarRecursosForestales(returnVal.Id);
                    returnVal.Solicitante = db2.Solicitantes.Consultar((Guid)recursos.amb_No_RNCId);
                    return returnVal;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
