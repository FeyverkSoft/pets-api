namespace Pets.Queries.Pages.Admin;

using Core.Mediatr;

/// <summary>
///     Запрос на получение страницы
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат страницы</param>
/// <param name="Page">Идентификатор страницы</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(Page)},
    ThrottlingTimeMs = 1000)]
public sealed record GetAdminPageQuery(Guid OrganisationId, String Page) : IRequest<AdminPageView?>;