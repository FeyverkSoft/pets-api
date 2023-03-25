namespace Pets.Queries.Pets;

using System.Collections.Generic;

using Types;

/// <summary>
///     Запрос на получение списка петомцев
/// </summary>
public sealed class GetPetsQuery : PageQuery<PetView>
{
    public GetPetsQuery(Guid organisationId, List<PetState> petStatuses, List<PetGender> genders, String? filter = null,
        Int32 offset = 0, Int32 limit = 8, Guid? petId = null)
        : base(offset, limit)
    {
        (OrganisationId, PetStatuses, PetId, Filter, Genders)
            = (organisationId, petStatuses, petId, filter, genders);
    }

    /// <summary>
    ///     Организация которой принадлежат петомцы
    /// </summary>
    public Guid OrganisationId { get; }

    /// <summary>
    ///     фильтр животных по статусам
    /// </summary>
    public List<PetState> PetStatuses { get; }

    /// <summary>
    ///     Идентификатор конкретного животного
    /// </summary>
    public Guid? PetId { get; }

    /// <summary>
    ///     Фильтр по полу
    /// </summary>
    public List<PetGender> Genders { get; }

    /// <summary>
    ///     Текстовый фильтр
    ///     Пока что по имени и краткому описанию
    /// </summary>
    public String? Filter { get; }
}