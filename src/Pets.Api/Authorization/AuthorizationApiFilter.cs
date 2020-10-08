using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pets.Api.Authorization
{
    public class AuthorizationApiFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpMethod = context.HttpContext.Request.Method;
            if (httpMethod.Equals(HttpMethod.Options.Method))
                return;

            if (context.Filters.All(f => f.GetType() != typeof(AuthorizeFilter)) ||
                context.Filters.Any(f => f.GetType() == typeof(AllowAnonymousFilter)))
            {
                return;
            }
            
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var scope = context.HttpContext.GetApiScope();
            if(scope?.Actions == null || scope.Actions.Count == 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if(!scope.Actions.Contains(httpMethod, StringComparer.InvariantCultureIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
