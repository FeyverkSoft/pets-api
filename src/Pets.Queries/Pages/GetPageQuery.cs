namespace Pets.Queries.Pages;

using Core.Mediatr;

/// <summary>
///     Запрос на получение страницы
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат петомцы</param>
/// <param name="Page">Идентификатор страницы</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(Page) },
    ThrottlingTimeMs = 2000)]
public sealed record GetPageQuery(Guid OrganisationId, String Page) : IRequest<PageView?>;