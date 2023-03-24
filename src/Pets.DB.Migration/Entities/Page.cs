namespace Pets.DB.Migrations.Entities;

using System;

internal sealed class Page
{
    public String Id { get; }

    /// <summary>
    ///     Идентификатор организации которой принадлежит контакт
    /// </summary>
    public Guid OrganisationId { get; }

    public Organisation Organisation { get; }

    /// <summary>
    ///     Ссылка на картинку шапки
    /// </summary>
    /// <typeparamref name="NVARCHAR(512) NOT NULL" />
    public String ImgLink { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    /// <typeparamref name="NVARCHAR(10240) NOT NULL" />
    public String MdBody { get; }

    public DateTime UpdateDate { get; }
    public DateTime CreateDate { get; }
    public Guid ConcurrencyTokens { get; set; }
}