using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using Pets.Types;

namespace Pets.Api.Authorization
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.FindFirst(x => x.Type.Equals(CustomClaimTypes.UserId)).Value);
        }
        public static String GetIp(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["X-Original-For"].ToString() ??
                   httpContext.Request.Headers["X-Forwarded-For"].ToString() ??
                   httpContext.Request.Headers["X-Real-IP"].ToString();
        }
        public static ScopeAction GetApiScope(this HttpContext httpContext)
        {
            return JsonConvert.DeserializeObject<ScopeAction>(httpContext.User.FindFirst(x => x.Type.Equals(CustomClaimTypes.Scope)).Value);
        }

        public sealed class ScopeAction
        {
            public ICollection<String> Actions { get; set; }
        }
    }
}
