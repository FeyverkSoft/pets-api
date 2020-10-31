using System;
using System.Collections.Generic;

using Query.Core;

namespace Pets.Queries.Organisation
{
    /// <summary>
    /// Запрос на получение списка нужд
    /// </summary>
    public sealed class GetNeedQuery : IQuery<IEnumerable<NeedView>>
    {
        /// <summary>
        /// Организация
        /// </summary>
        public Guid OrganisationId { get; }

        public GetNeedQuery(Guid organisationId)
            => (OrganisationId)
                = (organisationId);
    }
}