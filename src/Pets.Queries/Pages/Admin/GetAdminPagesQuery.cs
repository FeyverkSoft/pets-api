namespace Pets.Queries.Pages.Admin;

using Core.Mediatr;

/// <summary>
///     Запрос на получение страницы
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат страницы</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new []{nameof(OrganisationId), nameof(Limit), nameof(Offset)},
    ThrottlingTimeMs = 1000)]
public sealed record GetAdminPagesQuery(Guid OrganisationId, Int32 Limit, Int32 Offset) : PageQuery<AdminPageView>(Limit: Limit, Offset: Offset);