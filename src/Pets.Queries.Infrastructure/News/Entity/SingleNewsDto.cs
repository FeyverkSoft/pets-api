﻿namespace Pets.Queries.Infrastructure.News.Entity;

using System;

internal sealed class SingleNewsDto
{
    internal static readonly String Sql = @"
select
    n.Id,
    n.Title,
    n.Tags,
    n.ImgLink,
    n.MdShortBody,
    n.MdBody,
    _np.LinkedPets,
    n.CreateDate
from
    `News` n
left join (
    select
        np.NewsId, JSON_ARRAYAGG(JSON_OBJECT('Id', p.Id , 'Name', p.Name)) as LinkedPets
    from
        `NewsPets` np
    left join `Pet` p on
        p.Id = np.PetId 
    ) _np on  _np.NewsId = n.Id
where 1 = 1
    and n.OrganisationId = @OrganisationId
    and n.Id = @NewsId
";

    /// <summary>
    ///     Идентификатор новости
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Теги новости
    ///     JSON массив строк
    /// </summary>
    public String Tags { get; }

    /// <summary>
    ///     Ссылка на картинку шапки
    /// </summary>
    public String ImgLink { get; }

    /// <summary>
    ///     Предпросмотр новости в markdown
    /// </summary>
    public String MdShortBody { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String MdBody { get; }

    /// <summary>
    ///     Дата публикации новости
    /// </summary>
    public DateTime CreateDate { get; }

    /// <summary>
    ///     Json
    /// </summary>
    public String LinkedPets { get; set; }

    /// <summary>
    ///     Заголовок новости
    /// </summary>
    public String Title { get; set; }
}