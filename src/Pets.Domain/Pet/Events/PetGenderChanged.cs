namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

using Types;

public record PetGenderChanged(Guid PetId, PetGender Gender, PetGender OldGender, DateTime Date) : IEvent;