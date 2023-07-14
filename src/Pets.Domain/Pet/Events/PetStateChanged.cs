namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

using Types;

public record PetStateChanged(Guid PetId, PetState State, PetState OldState, DateTime Date) : IEvent;