using ProxyCrm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
namespace ProxyCrm.Filters
{
    public class AuthenticationFilter : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;

            if (req.Headers.GetValues("Authorization") != null)
            {
                var token = req.Headers.GetValues("Authorization").First();
                token = token.Substring(6);
                var Result = isValid(token);
                if (Result == 0)
                {
                    var currentPrincipal = new GenericPrincipal(new GenericIdentity(token), null);
                    context.Principal = currentPrincipal;
                }
                else if (Result == 1)
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
                else
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            }

            return Task.FromResult(0);
        }
        private int isValid(string token)
        {
            DateTime x = DateTime.UtcNow.AddMinutes(-30);
            var reqtoken = DbToken.Token.Where(DbToken => DbToken.Token == token).SingleOrDefault();
            if (reqtoken == null) {
                return 1;
            }

            else if (reqtoken.fecha < x) {
                DbToken.Token.Remove(reqtoken);
                return 2;
            }
            return 0;
        }
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}