namespace Pets.Queries.Pets.Admin;

using System.Collections.Generic;

using Core.Mediatr;

using Types;

/// <summary>
///     Запрос на получение списка петомцев
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат петомцы</param>
/// <param name="PetStatuses">фильтр животных по статусам</param>
/// <param name="Genders">Фильтр по полу</param>
/// <param name="Filter">Текстовый фильтр Пока что по имени и краткому описанию</param>
/// <param name="Offset"></param>
/// <param name="Limit"></param>
/// <param name="PetId">Идентификатор конкретного животного</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[]
        { nameof(OrganisationId), nameof(PetStatuses), nameof(Genders), nameof(Filter), nameof(PetId), nameof(Offset), nameof(Limit) },
    ThrottlingTimeMs = 50)]
public sealed record AdminGetPetsQuery(Guid OrganisationId, List<PetState> PetStatuses, List<PetGender> Genders, List<PetType> Types, String? Filter = null,
    Int32 Offset = 0, Int32 Limit = 8, Guid? PetId = null) : PageQuery<PetView>(Limit: Limit, Offset: Offset);