﻿namespace Pets.Queries.Pages;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Id</param>
/// <param name="OrganisationId">Id организации</param>
/// <param name="ImgLink">Ссылка на фотку до</param>
/// <param name="MdBody">Тело в markdown</param>
/// <param name="UpdateDate">Дата последнего изменения</param>
public sealed record PageView(String Id, Guid OrganisationId, String ImgLink, String? MdBody, DateTime UpdateDate);