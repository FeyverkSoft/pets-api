namespace Pets.Queries.News;

using Core.Mediatr;

/// <summary>
///     Получить единственную новость
/// </summary>
/// <param name="OrganisationId">Организация к которой относятся новости</param>
/// <param name="NewsId">идентификатор конкретной новости</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(NewsId) },
    ThrottlingTimeMs = 2000)]
public sealed record GetSingleNewsQuery(Guid OrganisationId, Guid NewsId) : IRequest<NewsView?>;