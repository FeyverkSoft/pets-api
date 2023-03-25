namespace Pets.Queries.Pages;

/// <summary>
///     Запрос на получение страницы
/// </summary>
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