using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyCrm.Models;
using ProxyCrm.Filters;

namespace ProxyCrm.Controllers
{
    public class SolicitantesController : ApiController
    {
        /// <summary>
        /// Loguea un solicitante en el sistema.
        /// </summary>
        /// <param name="solicitante">Credenciales del solicitante</param>
        /// <returns>Retorna un token valido por 30 min.</returns>
        /// POST: api/solicitantes/login
        [HttpPost]
        public MensajeRetorno<Tokens> Login(LoginSolicitante solicitante)
        {
            if (solicitante == null)
                return new MensajeRetorno<Tokens> { Data = null, Message = "Debe de suministrar los parametros", State = "FAIL" };
            CrmContext db = new CrmContext();
            var user = db.Solicitantes.Consultar(solicitante.Usuario);
            if (user == null) {
                return new MensajeRetorno<Tokens> { Data = null, Message = "El Usuario no existe", State = "FAIL" };
            }
            else {
                if (db.Solicitantes.CompararClave(solicitante.Clave, user.Clave))
                {
                    Tokens token = new Tokens();
                    token.fecha = DateTime.UtcNow;
                    token.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace('+', '%');
                    DbToken.Token.Add(token);
                    return new MensajeRetorno<Tokens> { Data = token, Message = "", State = "SUCCESS" }; ;
                }
                else
                {
                    return new MensajeRetorno<Tokens> { Data = null, Message = "La contraseña no es valida", State = "FAIL" };
                }
            }
        }
        /// <summary>
        /// Crea un usuario a un solicitante.
        /// </summary>
        /// <param name="solicitante">solicitante que se va crear</param>
        /// <returns>Retorna el solicitante creado.</returns>
        // POST: api/Solicitantes/crear
        [HttpPost]
        public MensajeRetorno<Solicitante> crear(Solicitante solicitante)
        {
            CrmContext db = new CrmContext();
            if (solicitante == null)
                return new MensajeRetorno<Solicitante> { Data = null, Message = "Debe de suministrar los parametros", State = "FAIL" };
            var result = db.Solicitantes.Crear(solicitante);
            if (result == null)
                return new MensajeRetorno<Solicitante> { Data = null, Message = "El usuario ya esta creado.", State = "FAIL" };
            if (result.CorreoElectronico == "")
                return new MensajeRetorno<Solicitante> { Data = null, Message = "Este correo esta siendo usado por otro usuario.", State = "FAIL" };
            result.Clave = "";
            return new MensajeRetorno<Solicitante> { Data = result, Message = "", State = "SUCESS" };
        }
        /// <summary>
        /// Cierra la session de un usuario logueado. remueve el token del usuario.
        /// </summary>
        /// <returns>Retorna un mensaje.</returns>
        [HttpPost]
        [AuthenticationFilter]
        //POST: api/logout
        public MensajeRetorno<string> Logout()
        {
            DbToken.Token.RemoveAt(DbToken.Token.FindIndex(dbtoken => dbtoken.Token == Request.Headers.GetValues("Authorization").First().Substring(6)));
            return new MensajeRetorno<string> { Data = "", Message = "ADIOS!", State = "SUCCESS" }; ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MensajeRetorno<Solicitante> Get(string id)
        {
            if (id == null)
                return new MensajeRetorno<Solicitante> { Data = null, Message = "Debe de suministrar los parametros", State = "FAIL" };
            CrmContext db = new CrmContext();
            Guid _id = new Guid();
            var result = new Solicitante();
            if (Guid.TryParse(id, out _id))
                result = db.Solicitantes.Consultar(_id);
            else
                result = db.Solicitantes.Consultar(id);
            if (result == null)
                return new MensajeRetorno<Solicitante> { Data = null, Message = "Esta cedula no existe.", State = "FAIL" };
            result.Clave = "";
            return new MensajeRetorno<Solicitante> { Data = result, Message = "", State = "SUCCESS" };

        }
        [HttpGet]
        public MensajeRetorno<Solicitante> GetNoAuth(string id) {
            if (id == null)
                return new MensajeRetorno<Solicitante> { Data = null, Message = "Debe de suministrar los parametros", State = "FAIL" };
            CrmContext db = new CrmContext();
            var result = db.Solicitantes.ConsultarNoRegistrado(id);
            if (result == null)
                return new MensajeRetorno<Solicitante> { Data = null, Message = "Esta cedula no existe.", State = "FAIL" };
            return new MensajeRetorno<Solicitante> { Data = result, Message = "", State = "SUCCESS" };
        }
    }
}
