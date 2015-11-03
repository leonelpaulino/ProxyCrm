using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDSolicitudes
        {
            /// <summary>
            /// Busca las solicitudes de un solicitante.s
            /// </summary>
            /// <param name="solicitante">Guid del solicitante</param>
            /// <returns>Retorna Todas las solicitudes de un solicitante</returns>
            public List<Solicitudes> ConsultarTodo(Guid solicitante)
            {
                CRMDataContext db = new CRMDataContext();

                CrmContext db2 = new CrmContext();
                var list = db.DBSolicitudEnLineas.Where(DBSolicitudEnLineas => DBSolicitudEnLineas.new_SolicitanteId == solicitante);
                if (list.Count() == 0)
                    return null;
                List<Solicitudes> Retu = new List<Solicitudes>();
                foreach (DBSolicitudEnLineas i in list)
                {
                    Retu.Add(new Solicitudes
                    {
                        CorreoElectronico = i.new_Correo_Electronico,
                        Direccion = i.new_Direccion,
                        CantParticipante = i.new_Cantidad_Participantes.Value,
                        Institucion = i.new_Institucion,
                        EstadoActual = db2.Estados.Consultar((Guid)i.new_EstadoId),
                        Telefono = i.new_Telefono,
                        Fecha = i.new_Solicitud_en_lineaBase.CreatedOn.Value,
                        Servicios = db2.Servicios.Consultar((Guid)i.new_ServiciosId),
                        Archivos = db2.Archivos.Consultar(i.new_Solicitud_en_lineaId),
                        Solicitantes = db2.Solicitantes.Consultar((Guid)i.new_SolicitanteId),
                        Id = i.new_Solicitud_en_lineaId,
                        Nota = i.new_Nota,
                        NoSolicitud = i.new_Solcitud_id
                    });
                }
                return Retu;
            }
            /// <summary>
            /// Crea una solicitud.
            /// </summary>
            /// <param name="record">Record que se va crear.</param>
            /// <returns>Retorna el guid del record que se creo .</returns>
            public Guid Crear(Solicitudes solicitud)
            {
                CrmProxy db = new CrmProxy();
                CrmContext db1 = new CrmContext();
                
                var estado = db1.Estados.ConsultarTodo(solicitud.Servicios.Id).OrderBy(es=>es.Posicion).FirstOrDefault();
                Entity newrow = new Entity("new_solicitud_en_linea");
                newrow["new_solicitanteid"] = new EntityReference("amb_solicitante",solicitud.Solicitantes.Id);
                newrow["new_institucion"] = solicitud.Institucion;
                newrow["new_direccion"] = solicitud.Direccion;
                newrow["new_telefono"] = solicitud.Telefono;
                newrow["new_correo_electronico"] = solicitud.CorreoElectronico;
                newrow["new_cantidad_participantes"] = solicitud.CantParticipante;
                newrow["new_serviciosid"] = new EntityReference("new_servicio",solicitud.Servicios.Id);
                newrow["new_estadoid"] = new EntityReference("new_estados",estado.Id);
                Guid solicitudid = db.Service.Create(newrow);
                foreach (Archivo i in solicitud.Archivos)
                {
                    Entity newAttchments = new Entity("annotation");
                    newAttchments["documentbody"] = Convert.ToBase64String(i.Contenido);
                    newAttchments["filename"] = i.NombreArchivo;
                    newAttchments["mimetype"] = i.TipoDeArchivo;
                    newAttchments["filesize"] = i.Longitud;
                    newAttchments["objectid"] = new EntityReference("new_solicitud_en_linea",solicitudid);
                    newAttchments["notetext"] = "Solicitud En Linea Adjunto";
                    db.Service.Create(newAttchments);
                }
                return solicitudid;
            }
            /// <summary>
            /// Actualiza una solicitud.
            /// </summary>
            /// <param name="record">record que se va actualizar</param>
            /// <returns>Retorna el guid del record que se actualizo</returns>
            public Guid Update(Solicitudes record)
            {
                return new Guid();
            }
            /// <summary>
            /// Busca una solicitud por Guid.
            /// </summary>
            /// <param name="solicitanteid">Guid de la solicitud</param>
            /// <returns>Retorna la solicitud</returns>
            public Solicitudes Consultar(Guid solicitudid)
            {
                CRMDataContext db = new CRMDataContext();
                CrmContext db2 = new CrmContext();

                var result = db.DBSolicitudEnLineas.Where(solicitud => solicitud.new_Solicitud_en_lineaId == solicitudid).SingleOrDefault();
                if (result == null)
                    return null;
                Solicitudes retur = new Solicitudes
                {
                    CorreoElectronico = result.new_Correo_Electronico,
                    Direccion = result.new_Direccion,
                    CantParticipante = result.new_Cantidad_Participantes.Value,
                    Institucion = result.new_Institucion,
                    EstadoActual = db2.Estados.Consultar((Guid)result.new_EstadoId),
                    Telefono = result.new_Telefono,
                    Fecha = result.new_Solicitud_en_lineaBase.CreatedOn.Value,
                    Servicios = db2.Servicios.Consultar((Guid)result.new_ServiciosId),
                    Archivos = db2.Archivos.Consultar(result.new_Solicitud_en_lineaId),
                    Solicitantes = db2.Solicitantes.Consultar((Guid)result.new_SolicitanteId),
                    Id = result.new_Solicitud_en_lineaId,
                    Nota = result.new_Nota,
                    NoSolicitud = result.new_Solcitud_id
                };
                return retur;
            }

        }
    }
}