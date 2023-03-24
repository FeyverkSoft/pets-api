namespace Pets.Types;

using System;

public static class CustomClaimTypes
{
    private const String ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

    /// <summary>
    ///     Логин юзвера
    /// </summary>
    public const String Login = ClaimTypeNamespace + "/login";

    /// <summary>
    ///     Права доступа юзверя
    /// </summary>
    public const String Scope = ClaimTypeNamespace + "/scope";

    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    public const String UserId = ClaimTypeNamespace + "/userId";

    /// <summary>
    ///     Идентификатор организации пользователя
    /// </summary>
    public const String OrganisationId = ClaimTypeNamespace + "/OrganisationId";
}