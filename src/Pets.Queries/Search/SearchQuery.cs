namespace Pets.Queries.Search;

using Core.Mediatr;

[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(Query), nameof(Offset), nameof(Limit)},
    ThrottlingTimeMs = 2000)]
public sealed class SearchQuery : PageQuery<SearchView>
{
    public SearchQuery(Guid organisationId, String query, Int32 offset = 0, Int32 limit = 8)
        : base(offset, limit)
    {
        (OrganisationId, Query)
            = (organisationId, query);
    }

    /// <summary>
    ///     Организация в которой выполняется поиск
    /// </summary>
    public Guid OrganisationId { get; }

    /// <summary>
    ///     Поисковый запрос
    /// </summary>
    public String Query { get; }
}