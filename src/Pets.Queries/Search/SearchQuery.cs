namespace Pets.Queries.Search;

using Core.Mediatr;

/// <summary>
/// </summary>
/// <param name="OrganisationId">Организация в которой выполняется поиск</param>
/// <param name="Query">Поисковый запрос</param>
/// <param name="Offset"></param>
/// <param name="Limit"></param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(Query), nameof(Offset), nameof(Limit) },
    ThrottlingTimeMs = 2000)]
public sealed record SearchQuery(Guid OrganisationId, String Query, Int32 Offset = 0, Int32 Limit = 8) : PageQuery<SearchView>(Limit: Limit, Offset: Offset);