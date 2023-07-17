namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

/// <summary>
/// Cобытие изменения описания животного в профайле
/// </summary>
/// <param name="PetId">Идентификатор питомца</param>
/// <param name="Date">Дата события</param>
/// <param name="Body">Новое тело описания</param>
public sealed record PetChangeDescription(Guid PetId, String Body, DateTime Date) : IEvent;