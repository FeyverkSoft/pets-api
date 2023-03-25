namespace Pets.Queries.News;

/// <summary>
///     Получить единственную новость
/// </summary>
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