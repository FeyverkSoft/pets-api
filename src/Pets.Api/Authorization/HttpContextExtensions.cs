﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using Pets.Domain.Authentication;
using Pets.Types;

namespace Pets.Api.Authorization
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.GetUserId();
        }
        public static Guid GetUserId(this ClaimsPrincipal User)
        {
            return Guid.Parse(User.FindFirst(x => x.Type.Equals(CustomClaimTypes.UserId)).Value);
        }
        
        public static Guid GetOrganisationId(this HttpContext httpContext)
        {
            return httpContext.User.GetOrganisationId();
        }
        public static Guid GetOrganisationId(this ClaimsPrincipal User)
        {
            return Guid.Parse(User.FindFirst(x => x.Type.Equals(CustomClaimTypes.OrganisationId)).Value);
        }
        
        public static String GetIp(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["X-Original-For"].ToString() ??
                   httpContext.Request.Headers["X-Forwarded-For"].ToString() ??
                   httpContext.Request.Headers["X-Real-IP"].ToString();
        }

        public static ScopeAction GetApiScope(this HttpContext httpContext)
        {
            var scope = httpContext.User.FindFirst(x => x.Type.Equals(CustomClaimTypes.Scope));
            return scope is null ? new ScopeAction() : JsonConvert.DeserializeObject<ScopeAction>(scope.Value);
        }
    }
}