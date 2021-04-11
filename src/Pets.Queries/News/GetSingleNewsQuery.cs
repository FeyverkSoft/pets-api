using System;

using Query.Core;

namespace Pets.Queries.News
{
    /// <summary>
    /// Получить единственную новость
    /// </summary>
    public sealed class GetSingleNewsQuery : IQuery<NewsView?>
    {
        /// <summary>
        /// Организация к которой относятся новости
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// идентификатор конкретной новости
        /// </summary>
        public Guid NewsId { get; }

        public GetSingleNewsQuery(Guid organisationId, Guid newsId)
            => (OrganisationId, NewsId)
                = (organisationId, newsId);
    }
}