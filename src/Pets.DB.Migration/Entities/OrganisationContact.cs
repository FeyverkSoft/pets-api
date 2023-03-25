namespace Pets.DB.Migrations.Entities;

using System;

using Types;

internal sealed class OrganisationContact
{
    public Guid Id { get; }

    /// <summary>
    ///     Идентификатор организации которой принадлежит контакт
    /// </summary>
    public Guid OrganisationId { get; }

    public Organisation Organisation { get; }

    /// <summary>
    ///     Тип контакта
    ///     email/phone/etc
    /// </summary>
    public ContactType Type { get; }

    /// <summary>
    ///     Ссылка на картинку
    /// </summary>
    public String? ImgLink { get; }

    /// <summary>
    ///     Markdown текст описание
    /// </summary>
    public String MdBody { get; }

    public Guid ConcurrencyTokens { get; }
}