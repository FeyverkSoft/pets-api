using System;

using Query.Core;

namespace Pets.Queries.Pets
{
    /// <summary>
    /// Запрос на получение списка петомцев
    /// </summary>
    public sealed class GetPetQuery : IQuery<PetView?>
    {
        /// <summary>
        /// Организация которой принадлежат петомцы
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Идентификатор конкретного животного
        /// </summary>
        public Guid PetId { get; }

        public GetPetQuery(Guid organisationId, Guid petId)
            => (OrganisationId, PetId)
                = (organisationId, petId);
    }
}