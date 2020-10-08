using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Pets.Api.Authorization
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimType.UserId)).Value);
        }
        public static String GetIp(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["X-Original-For"].ToString() ??
                   httpContext.Request.Headers["X-Forwarded-For"].ToString() ??
                   httpContext.Request.Headers["X-Real-IP"].ToString();
        }
        public static Guid GetSessionId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
        }

        public static ScopeAction GetApiScope(this HttpContext httpContext)
        {
            return JsonConvert.DeserializeObject<ScopeAction>(httpContext.User.FindFirst(x => x.Type.Equals(ClaimType.ApiScope)).Value);
        }

        public sealed class ScopeAction
        {
            public ICollection<String> Actions { get; set; }
        }
    }
}
