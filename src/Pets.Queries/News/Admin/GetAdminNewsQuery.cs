namespace Pets.Queries.News.Admin;

using System.Collections.Generic;

using Core.Mediatr;

using Types;

/// <summary>
///     Запрос на получение списка новостей от имени админа
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат петомци</param>
/// <param name="PetId">Фильтр по конкретному животному</param>
/// <param name="Filter">Текстовый фильтр по описанию и/или краткому описанию</param>
/// <param name="Offset"></param>
/// <param name="Limit"></param>
/// <param name="NewsId">Идентификатор конкретного животного</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[]
        { nameof(OrganisationId), nameof(NewsStatuses), nameof(Filter), nameof(NewsId), nameof(Offset), nameof(Limit) },
    ThrottlingTimeMs = 50)]
public sealed record GetAdminNewsQuery(Guid OrganisationId, List<NewsState>? NewsStatuses = null, Guid? PetId = null, String? Tag = null, String? Filter = null,
    Int32 Offset = 0, Int32 Limit = 8, Guid? NewsId = null) : PageQuery<AdminNewsView>(Limit: Limit, Offset: Offset);