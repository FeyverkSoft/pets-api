namespace Pets.Queries.News;

using Core.Mediatr;

/// <summary>
///     Получить единственную новость
/// </summary>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(NewsId) },
    ThrottlingTimeMs = 2000)]
public sealed class GetSingleNewsQuery : IRequest<NewsView?>
{
    public GetSingleNewsQuery(Guid organisationId, Guid newsId)
    {
        (OrganisationId, NewsId)
            = (organisationId, newsId);
    }

    /// <summary>
    ///     Организация к которой относятся новости
    /// </summary>
    public Guid OrganisationId { get; }

    /// <summary>
    ///     идентификатор конкретной новости
    /// </summary>
    public Guid NewsId { get; }
}