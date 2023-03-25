namespace Pets.Api.Models.Admin.Authorization;

using Microsoft.AspNetCore.Mvc;

using Types;

/// <summary>
///     модель для авторизации юзера
///     <see cref="https://tools.ietf.org/html/rfc6749" />
/// </summary>
public sealed class AuthBinding
{
    /// <summary>
    /// </summary>
    [FromForm(Name = "grant_type")]
    public GrantType GrantType { get; set; }

    /// <summary>
    ///     Ник
    /// </summary>
    [FromForm(Name = "username")]
    public String? UserName { get; set; }

    /// <summary>
    ///     пароль
    /// </summary>
    [FromForm(Name = "password")]
    public String? Password { get; set; }

    /// <summary>
    ///     рефреш токен
    /// </summary>
    [FromForm(Name = "refresh_token")]
    public String? RefreshToken { get; set; }
}