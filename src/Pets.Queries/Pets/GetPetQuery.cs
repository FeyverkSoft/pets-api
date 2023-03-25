namespace Pets.Queries.Pets;

using Core.Mediatr;

/// <summary>
///     Запрос на получение списка петомцев
/// </summary>
[MediatRDedublicateExecution(
    KeyPropertyNames = new[] { nameof(OrganisationId), nameof(PetId)},
    ThrottlingTimeMs = 2000)]
public sealed class GetPetQuery : IRequest<PetView?>
{
    public GetPetQuery(Guid organisationId, Guid petId)
    {
        (OrganisationId, PetId)
            = (organisationId, petId);
    }

    /// <summary>
    ///     Организация которой принадлежат петомцы
    /// </summary>
    public Guid OrganisationId { get; }

    /// <summary>
    ///     Идентификатор конкретного животного
    /// </summary>
    public Guid PetId { get; }
}