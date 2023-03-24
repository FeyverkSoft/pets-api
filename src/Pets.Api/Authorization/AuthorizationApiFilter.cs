namespace Pets.Api.Authorization;

using System.Linq;
using System.Net.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizationApiFilter : IAsyncAuthorizationFilter
{
    private static readonly String[] ReadMethods = { HttpMethod.Get.Method };
    private static readonly String[] WriteMethods = { HttpMethod.Post.Method, HttpMethod.Put.Method, HttpMethod.Delete.Method, HttpMethod.Patch.Method };

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpMethod = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path.Value; // возможно стоит посмотреть в сторону context.RouteData

        if (httpMethod.Equals(HttpMethod.Options.Method))
            return;

        if (context.Filters.All(f => f.GetType() != typeof(AuthorizeFilter)) ||
            context.Filters.Any(f => f.GetType() == typeof(AllowAnonymousFilter)))
            return;

        var user = context.HttpContext.User;
        if (user == null || !user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var scope = context.HttpContext.GetApiScope();

        if (ReadMethods.Any(_ => _.Equals(httpMethod)))
        {
            if (scope.ReadRequests.Contains("*"))
                return;
            if (scope.ReadRequests.Contains(path, StringComparer.InvariantCultureIgnoreCase))
                return;
        }

        if (WriteMethods.Any(_ => _.Equals(httpMethod)))
        {
            if (scope.WriteRequests.Contains("*"))
                return;
            if (scope.WriteRequests.Contains(path, StringComparer.InvariantCultureIgnoreCase))
                return;
        }

        context.Result = new UnauthorizedResult();
    }
}