using System;

using Rabbita.Core;

namespace Pets.Domain.Pet.Events
{
    public class PetAnimalIdChanged : IEvent
    {
        public Guid PetId { get; }
        public Decimal? AnimalId { get; }
        public Decimal? OldAnimalId { get; }
        public DateTime Date { get; }

        public PetAnimalIdChanged(Guid petId, Decimal? animalId, Decimal? oldAnimalId, DateTime date)
            => (PetId, AnimalId, OldAnimalId, Date)
                = (petId, animalId, oldAnimalId, date);
    }
}