using System;
using System.Collections.Generic;

using Query.Core;

namespace Pets.Queries.Organisation
{
    /// <summary>
    /// Запрос на получение списка контактной информации
    /// </summary>
    public sealed class GetContactsQuery : IQuery<IEnumerable<ContactView>>
    {
        /// <summary>
        /// Организация которой принадлежат реквезиты
        /// </summary>
        public Guid OrganisationId { get; }

        public GetContactsQuery(Guid organisationId)
            => (OrganisationId)
                = (organisationId);
    }
}