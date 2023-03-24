namespace Pets.Domain.Pet.Events;

using Rabbita.Core;

using Types;

public class PetGenderChanged : IEvent
{
    public PetGenderChanged(Guid petId, PetGender gender, PetGender oldGender, DateTime date)
    {
        (PetId, Gender, OldGender, Date)
            = (petId, gender, oldGender, date);
    }

    public Guid PetId { get; }
    public PetGender Gender { get; }
    public PetGender OldGender { get; }
    public DateTime Date { get; }
}