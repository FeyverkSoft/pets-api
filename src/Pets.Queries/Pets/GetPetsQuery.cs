using System;
using System.Collections.Generic;

using Pets.Types;

namespace Pets.Queries.Pets
{
    /// <summary>
    /// Запрос на получение списка петомцев
    /// </summary>
    public sealed class GetPetsQuery : PageQuery<PetView>
    {
        /// <summary>
        /// Организация которой принадлежат петомцы
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// фильтр животных по статусам
        /// </summary>
        public List<PetState> PetStatuses { get; }

        /// <summary>
        /// Идентификатор конкретного животного
        /// </summary>
        public Guid? PetId { get; }

        public GetPetsQuery(Guid organisationId, List<PetState> petStatuses, Int32 offset = 0, Int32 limit = 8, Guid? petId = null)
            : base(offset, limit)
            => (OrganisationId, PetStatuses, PetId)
                = (organisationId, petStatuses, petId);
    }
}