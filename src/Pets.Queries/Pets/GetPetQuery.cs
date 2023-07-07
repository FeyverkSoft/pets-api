namespace Pets.Queries.Pets;

using Core.Mediatr;

/// <summary>
///     Запрос на получение списка петомцев
/// </summary>
/// <param name="OrganisationId">Организация которой принадлежат петомцы</param>
/// <param name="PetId">Идентификатор конкретного животного</param>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(PetId)},
    ThrottlingTimeMs = 2000)]
public sealed record GetPetQuery(Guid OrganisationId, Guid PetId) : IRequest<PetView?>;