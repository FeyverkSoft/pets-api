using System;

namespace Pets.Types
{
    public static class CustomClaimTypes
    {
        private const String ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

        /// <summary>
        /// Логин юзвера
        /// </summary>
        public const String Login = ClaimTypeNamespace + "/login";

        /// <summary>
        /// Права доступа юзверя
        /// </summary>
        public const String Scope = ClaimTypeNamespace + "/scope";
    }
}