﻿namespace Pets.Queries.News;

using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Идентификатор новости</param>
/// <param name="Title">Заголовок новости</param>
/// <param name="ImgLink">Ссылка на картинку шапки</param>
/// <param name="MdShortBody">Предпросмотр новости в markdown</param>
/// <param name="MdBody">Тело в markdown</param>
/// <param name="CreateDate">Дата публикации новости</param>
/// <param name="LinkedPets"></param>
/// <param name="Tags">Теги новости</param>
public sealed record NewsView(Guid Id, String Title, String ImgLink, String MdShortBody, String MdBody, DateTime CreateDate, ICollection<LinkedPetsView> LinkedPets,
    ICollection<String> Tags);