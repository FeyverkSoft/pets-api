namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

using Types;

public sealed record PetTypeChanged(Guid PetId, PetType type, PetType oldType, DateTime date) : IEvent;