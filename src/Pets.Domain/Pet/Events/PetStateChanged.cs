using System;

using Pets.Types;

using Rabbita.Core;

namespace Pets.Domain.Pet.Events
{
    public class PetStateChanged : IEvent
    {
        public Guid PetId { get; }
        public PetState State { get; }
        public PetState OldState { get; }
        public DateTime Date { get; }

        public PetStateChanged(Guid petId, PetState state, PetState oldState, DateTime date)
            => (PetId, State, OldState, Date)
                = (petId, state, oldState, date);
    }
}