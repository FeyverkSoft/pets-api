namespace Pets.Domain.Authentication;

/// <summary>
///     пользователь системы
/// </summary>
public sealed class User
{
    /// <summary>
    ///     идентификатор пользователя
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     login пользователя
    /// </summary>
    public String Login { get; }

    /// <summary>
    ///     имя пользователя
    /// </summary>
    public String Name { get; }

    /// <summary>
    ///     хеш пароля пользователя
    /// </summary>
    public String PasswordHash { get; private set; }

    /// <summary>
    ///     JSON array с правами доступа юзера
    /// </summary>
    public ScopeAction Permissions { get; }

    /// <summary>
    ///     Состояние юзера, активен/забанен/требуется подтверждение мыла
    /// </summary>
    public String State { get; }

    public Guid ConcurrencyToken { get; private set; } = Guid.NewGuid();

    /// <summary>
    ///     Организация которой принадлежит юзер
    /// </summary>
    public Guid OrganisationId { get; private set; }

    public void SetPasswordHash(String passwordHash)
    {
        PasswordHash = passwordHash;
        ConcurrencyToken = Guid.NewGuid();
    }
}