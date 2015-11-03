using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ProxyCrm.Models
{
    partial class CrmContext
    {
        public class CRUDSolicitante
        {
            /// <summary>
            /// Variable para buscar autenticados y no autenticados.
            /// </summary>
            private bool Autenticado = false;
            /// <summary>
            /// Variable para buscar todos los cedulados (esten autenticados o no ).
            /// </summary>
            private bool Todo = false;
            /// <summary>
            /// Busca un solicitante por la cedula.
            /// </summary>
            /// <param name="cedula"></param>
            /// <returns>el solicitante o nulo si no encuentra ningun solicitante </returns>
            public Solicitante Consultar(string cedula)
            {
                CRMDataContext db = new CRMDataContext();
                SolicitudesEnlineaDataContext db2 = new SolicitudesEnlineaDataContext();
                CRUDSolicitudes db3 = new CRUDSolicitudes();
                DBSolicitantes result = null;
                Usuario pass = new Usuario();
                pass.Clave = "";
                if (Todo)
                    result = db.DBSolicitantes.Where(solicitantes => solicitantes.amb_no_documento == cedula ).SingleOrDefault();
                else if (!Autenticado)
                    result = db.DBSolicitantes.Where(solicitantes => solicitantes.amb_no_documento == cedula && solicitantes.new_Usuario_Pagina == true).SingleOrDefault();
                else
                    result = db.DBSolicitantes.Where(solicitantes => solicitantes.amb_no_documento == cedula).SingleOrDefault();
                if (result == null || pass == null)
                    return null;
                if (!Autenticado && !Todo)
                    pass = db2.Usuarios.Where(Usuario => Usuario.usuario1 == cedula).SingleOrDefault();
                Autenticado = false;
                Solicitante Retu = new Solicitante();
                Retu.Cedula = result.amb_no_documento;
                Retu.CorreoElectronico = result.new_CorreoElectrnico1;
                Retu.NombreCompleto = result.amb_nombre;
                Retu.Clave = pass.Clave;
                Retu.Telefono = result.new_NumerodeTelefono1;
                Retu.Id = result.amb_solicitanteId;
                return Retu;
            }
            /// <summary>
            /// Busca un solicitante el guid.
            /// </summary>
            /// <param name="cedula"></param>
            /// <returns>el solicitante o nulo si no encuentra ningun solicitante </returns>
            public Solicitante Consultar(Guid solicitanteid)
            {
                CRMDataContext db = new CRMDataContext();
                CRUDSolicitudes db2 = new CRUDSolicitudes();

                var result = db.DBSolicitantes.Where(solicitantes => solicitantes.amb_solicitanteId == solicitanteid).SingleOrDefault();
                if (result == null)
                    return null;

                Solicitante Retu = new Solicitante();
                Retu.Cedula = result.amb_no_documento;
                Retu.CorreoElectronico = result.new_CorreoElectrnico1;
                Retu.NombreCompleto = result.amb_nombre;
                Retu.Id = result.amb_solicitanteId;
                return Retu;
            }
            /// <summary>
            /// Consultar cedulados que no estan registrados en la pagina del ministerio.
            /// </summary>
            /// <param name="cedula"></param>
            /// <returns></returns>
            public Solicitante ConsultarNoRegistrado(string cedula) {
                Autenticado = true;
               return  Consultar(cedula);
                
            }
            /// <summary>
            /// Consultar todos los cedulados esten registrados en la pagina o no.
            /// </summary>
            /// <param name="cedula"></param>
            /// <returns></returns>
            public Solicitante ConsultarTodos(string cedula) {
                Todo = true;
                return Consultar(cedula);
            }
            /// <summary>
            /// Crea un solicitante.
            /// </summary>
            /// <param name="record">Record que se va crear.</param>
            /// <returns>Retorna el guid del record que se creo .</returns>
            public Solicitante Crear(Solicitante record)
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    SolicitudesEnlineaDataContext db2 = new SolicitudesEnlineaDataContext();
                    DBSolicitantes updaterow = db.DBSolicitantes.Where(DBSolicitantes => DBSolicitantes.amb_no_documento == record.Cedula).SingleOrDefault();
                    if (updaterow == null || updaterow.new_Usuario_Pagina.Value )
                        return null;
                    if (db2.Usuarios.Any(soli => soli.Correo == record.CorreoElectronico))
                        return new Solicitante { CorreoElectronico = "" };
                    updaterow.new_Usuario_Pagina = true;
                    updaterow.new_CorreoElectrnico1 = record.CorreoElectronico;
                    db2.Usuarios.InsertOnSubmit(new Usuario { Correo = record.CorreoElectronico, usuario1 = record.Cedula, Clave = HashSHA1(record.Clave) });
                    db2.SubmitChanges();
                    db.SubmitChanges();
                    record.Id = updaterow.amb_solicitanteId;
                    record.Clave = "";

                    return record;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Hashea un string
            /// </summary>
            /// <param name="pass">string que se va hashear</param>
            /// <returns>retorna el string hasheado</returns>
            private string HashSHA1(string pass) {
                var sha1 = System.Security.Cryptography.SHA1.Create();
                var inputBytes = Encoding.ASCII.GetBytes(pass);
                var hash = sha1.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                for (var i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            /// <summary>
            /// Actualiza un solicitante.
            /// </summary>
            /// <param name="record">record que se va actualizar</param>
            /// <returns>Retorna el guid del record que se actualizo</returns>
            public Solicitante Update(Solicitante record)
            {
                try
                {
                    CRMDataContext db = new CRMDataContext();
                    SolicitudesEnlineaDataContext db2 = new SolicitudesEnlineaDataContext();

                    Usuario updaterow1 = db2.Usuarios.Where(Usuario => Usuario.usuario1 == record.Cedula).SingleOrDefault();
                    if (updaterow1 == null)
                        return null;
                    DBSolicitantes updaterow2 = db.DBSolicitantes.Where(DBSolicitantes => DBSolicitantes.amb_solicitanteId == record.Id).SingleOrDefault();
                    if (updaterow2 == null)
                        return null;
                    updaterow2.new_CorreoElectrnico1 = record.CorreoElectronico;
                    updaterow1.Clave = HashSHA1(record.Clave);
                    updaterow1.Correo = record.CorreoElectronico;
                    db.DBSolicitantes.InsertOnSubmit(updaterow2);
                    db2.Usuarios.InsertOnSubmit(updaterow1);
                    record.Clave = "";
                    return record;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Compara dos contraseña
            /// </summary>
            /// <param name="pass1">Contraseña sin encriptar</param>
            /// <param name="pass2">Contraseña encriptada</param>
            /// <returns></returns>
            public bool CompararClave(string pass1, string pass2)
            {
                return pass2 == HashSHA1(pass1);
            }
        }
    }
}