﻿namespace Pets.DB.Migrations.Entities;

using System;
using System.Collections.Generic;

using Types;

internal sealed class News
{
    /// <summary>
    ///     Идентификатор новости
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Заголовок новости
    /// </summary>
    public String Title { get; }

    /// <summary>
    ///     Идентификатор организации которой принадлежит новость
    /// </summary>
    public Guid OrganisationId { get; }

    public Organisation Organisation { get; }

    /// <summary>
    ///     Теги новости
    /// </summary>
    public String Tags { get; }

    /// <summary>
    ///     Ссылка на картинку шапки
    /// </summary>
    /// <typeparamref name="NVARCHAR(512) NOT NULL" />
    public String ImgLink { get; }

    /// <summary>
    ///     Предпросмотр новости в markdown
    /// </summary>
    /// <typeparamref name="NVARCHAR(512) NOT NULL" />
    public String MdShortBody { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    /// <typeparamref name="NVARCHAR(10240) NOT NULL" />
    public String MdBody { get; }

    public DateTime UpdateDate { get; }
    public DateTime CreateDate { get; }
    public Guid ConcurrencyTokens { get; }

    /// <summary>
    ///     Список Петов в новости
    /// </summary>
    public IEnumerable<NewsPets> NewsPets { get; }

    /// <summary>
    /// Видимость новости
    /// </summary>
    public NewsState State { get; set; }
}