using Microsoft.Xrm.Sdk;
using ProxyCrm.Models.CrmCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDPagoTematico
        {
            private Guid CrearBase()
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    amb_pago_tematicoBase baserecord = new amb_pago_tematicoBase();
                    baserecord.amb_pago_tematicoId = Guid.NewGuid();
                    baserecord.CreatedOn = DateTime.Now;
                    baserecord.CreatedBy = BaseConfiguration.userId;
                    baserecord.OwnerId = BaseConfiguration.userId;
                    baserecord.OwningBusinessUnit = BaseConfiguration.OwningUnit;
                    baserecord.statecode = 1;
                    baserecord.OwnerIdType = 8;
                    baserecord.ModifiedOn = DateTime.Now;
                    baserecord.ModifiedBy = BaseConfiguration.userId;
                    db.amb_pago_tematicoBases.InsertOnSubmit(baserecord);
                    return baserecord.amb_pago_tematicoId;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            public Guid Crear(PagoTematico record) {

                try
                {
                     CrmProxy db = new CrmProxy();
                    //CRMDataContext db = new CRMDataContext();
                     CrmContext db2 = new CrmContext();
                    //DBPagoTematico pagotematico = new DBPagoTematico();
                    //pagotematico.amb_pago_tematicoId = CrearBase();
                    //pagotematico.amb_name = record.Solicitante.NombreCompleto;
                    //pagotematico.amb_cheque_no = record.Cheque;
                    //pagotematico.amb_telefono = record.Solicitante.Telefono;
                    //pagotematico.amb_area_tematica = record.IdAreaTematica;
                    //pagotematico.amb_concepto_apb = record.ConceptoRF;
                    //pagotematico.amb_observaciones = record.Observaciones;
                    //pagotematico.amb_pies_tablar = record.PiesTablar;
                    //pagotematico.amb_Cantidad_float_m3 = record.M3;
                    //pagotematico.amb_monto = record.MontoRd;
                    //if (db2.RecursosForestales.Existe(record.Recursos))
                    //{
                    //    pagotematico.amb_RecursosForestalesId = db2.RecursosForestales.Update(record.Recursos);
                    //}
                    //else
                    //{
                    //    pagotematico.amb_RecursosForestalesId = db2.RecursosForestales.Crear(record.Recursos);
                    //}
                    //db.DBPagoTematicos.InsertOnSubmit(pagotematico);
                    //record.Detalle.PagoTematico = pagotematico.amb_pago_tematicoId;
                    //db2.DetalleExportacionImportacion.Crear(record.Detalle);
                    //return record.Id;
                    Entity PagoTematico = new Entity("amb_pago_tematico");
                    PagoTematico["amb_no_documento_id"] = new EntityReference("amb_solicitante", record.Solicitante.Id);
                    PagoTematico["amb_name"] = record.Solicitante.NombreCompleto;
                    PagoTematico["amb_cheque_no"] = record.Cheque;
                    PagoTematico["amb_telefono"] = record.Solicitante.Telefono;
                    PagoTematico["emailaddress"] = record.Solicitante.CorreoElectronico;
                    PagoTematico["amb_area_tematica"] = new OptionSetValue(record.IdAreaTematica);
                    PagoTematico["amb_concepto_apb"] = new OptionSetValue(record.ConceptoRF);
                    record.Id = db.Service.Create(PagoTematico);
                    record.Detalle.PagoTematico = record.Id;
                    db2.DetalleExportacionImportacion.Crear(record.Detalle);
                    PagoTematico["amb_observaciones"] = record.Observaciones;
                    PagoTematico["amb_pies_tablar"] = record.M3;
                    PagoTematico["amb_cantidad_float_m3"] = record.PiesTablar;
                    PagoTematico["amb_monto"] = record.MontoRd;
                    PagoTematico.Id = record.Id;
                    db.Service.Update(PagoTematico);
                    return record.Id;
                }
                catch (Exception e) {
                    throw e;
                }
            }
            public PagoTematico Consultar(Guid id) {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    CrmContext db2 = new CrmContext();
                    var pagotematico = db.DBPagoTematicos.Where(pt => pt.amb_pago_tematicoId == id).SingleOrDefault();
                    if (pagotematico == null)
                        return null;
                    PagoTematico returnVal = new PagoTematico();
                    returnVal.AreaTematica = pagotematico.amb_area_tematica == null? 0: (int)pagotematico.amb_area_tematica;
                    returnVal.Cheque = pagotematico.amb_cheque_no;
                    returnVal.CodigoTarifa = pagotematico.amb_codigo_tarifa;
                    returnVal.ConceptoRF = pagotematico.amb_concepto_apb == null ?0 : (int)pagotematico.amb_concepto_apb;
                    returnVal.Detalle = db2.DetalleExportacionImportacion.ConsultarPagoTematico(pagotematico.amb_pago_tematicoId);
                    returnVal.Id = pagotematico.amb_pago_tematicoId;
                    returnVal.M3 = pagotematico.amb_Cantidad_float_m3 == null?0: (double)pagotematico.amb_Cantidad_float_m3;
                    returnVal.Observaciones = pagotematico.amb_observaciones;
                    returnVal.Recursos = db2.RecursosForestales.Consultar((Guid)pagotematico.amb_RecursosForestalesId);
                    returnVal.Solicitante = db2.Solicitantes.Consultar((Guid)pagotematico.new_RNCEmpresa);
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