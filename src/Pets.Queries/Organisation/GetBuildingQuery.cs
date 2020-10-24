using System;
using System.Collections.Generic;

using Query.Core;

namespace Pets.Queries.Organisation
{
    /// <summary>
    /// Запрос на получение списка стройматериалов
    /// </summary>
    public sealed class GetBuildingQuery : IQuery<IEnumerable<ResourceView>>
    {
        /// <summary>
        /// Организация которой принадлежат строй материалы
        /// </summary>
        public Guid OrganisationId { get; }

        public GetBuildingQuery(Guid organisationId)
            => (OrganisationId)
                = (organisationId);
    }
}