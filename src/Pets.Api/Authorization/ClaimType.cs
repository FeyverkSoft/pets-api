using System;

namespace Pets.Api.Authorization
{
    public static class ClaimType
    {
        public const String UserId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Pets.UserId";
        public const String ApiScope = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Pets.ApiScope";
    }
}
