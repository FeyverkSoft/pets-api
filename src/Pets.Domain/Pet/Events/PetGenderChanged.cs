using System;

using Pets.Types;

using Rabbita.Core;

namespace Pets.Domain.Pet.Events
{
    public class PetGenderChanged : IEvent
    {
        public Guid PetId { get; }
        public PetGender Gender { get; }
        public PetGender OldGender { get; }
        public DateTime Date { get; }

        public PetGenderChanged(Guid petId, PetGender gender, PetGender oldGender, DateTime date)
            => (PetId, Gender, OldGender, Date) 
                = (petId, gender, oldGender, date);
    }
}