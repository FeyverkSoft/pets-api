using System;

using Query.Core;

namespace Pets.Queries.Pages
{
    /// <summary>
    /// Запрос на получение страницы
    /// </summary>
    public sealed class GetPageQuery : IQuery<PageView?>
    {
        /// <summary>
        /// Организация которой принадлежат петомцы
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Идентификатор страницы
        /// </summary>
        public String Page { get; }

        public GetPageQuery(Guid organisationId, String page)
            => (OrganisationId, Page)
                = (organisationId, page);
    }
}