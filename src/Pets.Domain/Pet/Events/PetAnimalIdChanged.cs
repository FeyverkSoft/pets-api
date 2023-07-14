namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

public record PetAnimalIdChanged (Guid PetId, Decimal? AnimalId, Decimal? OldAnimalId, DateTime Date): IEvent;