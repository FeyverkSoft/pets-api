namespace Pets.Queries.News.Admin;

/// <summary>
/// Запрос на получение единственной новости
/// </summary>
/// <param name="OrganisationId"></param>
/// <param name="NewsId"></param>
public sealed record GetAdminSingleNewsQuery(Guid OrganisationId, Guid NewsId) : IRequest<AdminNewsView?>;