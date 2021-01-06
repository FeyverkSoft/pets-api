using System;

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
        /// некий фильтр, пока что не понятно какие параметры
        /// </summary>
        public String? Filter { get; }

        /// <summary>
        /// Идентификатор конкретного животного
        /// </summary>
        public Guid? PetId { get; }

        public GetPetsQuery(Guid organisationId, Int32 offset = 0, Int32 limit = 8, Guid? petId = null, String? filter = null)
            : base(offset, limit)
            => (OrganisationId, Filter, PetId)
                = (organisationId, filter, petId);
    }
}