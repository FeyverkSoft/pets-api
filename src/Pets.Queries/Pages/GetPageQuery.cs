namespace Pets.Queries.Pages;

using Core.Mediatr;

/// <summary>
///     Запрос на получение страницы
/// </summary>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(Page)},
    ThrottlingTimeMs = 2000)]
public sealed class GetPageQuery : IRequest<PageView?>
{
    public GetPageQuery(Guid organisationId, String page)
    {
        (OrganisationId, Page)
            = (organisationId, page);
    }

    /// <summary>
    ///     Организация которой принадлежат петомцы
    /// </summary>
    public Guid OrganisationId { get; }

    /// <summary>
    ///     Идентификатор страницы
    /// </summary>
    public String Page { get; }
}