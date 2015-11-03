using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProxyCrm.Models;
namespace ProxyCrm.Controllers
{
    public class TokensController : ApiController
    {
        // GET: api/Tokens
        public  MensajeRetorno<bool> Get(string id)
        {
            if (DbToken.Token.Any(token => token.Token == id))
                return new MensajeRetorno<bool> { Data = true, Message = "", State = "SUCCESS" };
            else
                return new MensajeRetorno<bool> { Data = false, Message = "Token no existe", State = "FAIL" };
        }
    }
}
