namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

using Types;

public class PetStateChanged : IEvent
{
    public PetStateChanged(Guid petId, PetState state, PetState oldState, DateTime date)
    {
        (PetId, State, OldState, Date)
            = (petId, state, oldState, date);
    }

    public Guid PetId { get; }
    public PetState State { get; }
    public PetState OldState { get; }
    public DateTime Date { get; }
}