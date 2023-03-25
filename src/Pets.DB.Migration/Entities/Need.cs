namespace Pets.DB.Migrations.Entities;

using System;

internal sealed class Need
{
    public String Id { get; }

    /// <summary>
    ///     Идентификатор организации которой принадлежит контакт
    /// </summary>
    public Guid OrganisationId { get; }

    public Organisation Organisation { get; }

    /// <summary>
    ///     Ссылка на картинки
    ///     JSON
    /// </summary>
    public String ImgLinks { get; }

    /// <summary>
    ///     Markdown текст описание
    /// </summary>
    public String MdBody { get; }

    /// <summary>
    ///     Статус
    /// </summary>
    public String NeedState { get; }

    public Guid ConcurrencyTokens { get; }
    public Guid ConcurrencyToken { get; set; }
}