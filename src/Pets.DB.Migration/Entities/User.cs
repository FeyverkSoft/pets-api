namespace Pets.DB.Migrations.Entities;

using System;

/// <summary>
///     пользователь системы
/// </summary>
internal sealed class User
{
    /// <summary>
    ///     идентификатор пользователя
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Идентификатор организации которой принадлежит сотрудник
    /// </summary>
    public Guid OrganisationId { get; }

    public Organisation Organisation { get; }

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
    public String PasswordHash { get; }

    /// <summary>
    ///     JSON с правами доступа юзера
    /// </summary>
    public String Permissions { get; }

    /// <summary>
    ///     Состояние юзера, активен/забанен/требуется подтверждение мыла
    /// </summary>
    public String State { get; }

    public Guid ConcurrencyToken { get; }
}