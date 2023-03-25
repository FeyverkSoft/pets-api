namespace Pets.Infrastructure.Authentication;

/// <summary>
///     Параметры выдачи jwt токена
/// </summary>
public record JwtAuthOptions
{
    /// <summary>
    ///     Издатель токена
    /// </summary>
    public String Issuer { get; set; } = "Pets.Auth";

    /// <summary>
    ///     Пользователи токена
    /// </summary>
    public String? Audience { get; set; } = "Pets.*";

    /// <summary>
    ///     Ключ для подписания токена
    /// </summary>
    public String? SecretKey { get; set; } = null;

    /// <summary>
    ///     Время жизни токена
    /// </summary>
    public Int32 LifeTimeMinutes { get; set; } = 10;
}