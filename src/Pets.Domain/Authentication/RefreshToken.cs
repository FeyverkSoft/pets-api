namespace Pets.Domain.Authentication;

/// <summary>
///     сущность токенов для перевыпуска JWT токена
/// </summary>
public sealed class RefreshToken
{
    protected RefreshToken()
    {
    }

    public RefreshToken(String id, Guid userId, String ip, DateTime expireDate)
    {
        Id = id;
        IpAddress = ip;
        UserId = userId;
        ExpireDate = expireDate;
    }

    /// <summary>
    ///     id токена обновления сессии
    /// </summary>
    public String Id { get; }

    /// <summary>
    ///     id юзера которому принадлежит токен
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    ///     Ip которому был выдан токен
    /// </summary>
    public String IpAddress { get; }

    /// <summary>
    ///     Дата стухания
    /// </summary>
    public DateTime ExpireDate { get; private set; }

    public void Terminate()
    {
        ExpireDate = DateTime.UtcNow.AddSeconds(15); // дадим 10 секунд на просраться, запросам с фронт систем.
    }
}