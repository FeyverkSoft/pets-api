namespace Pets.Domain.Pet.Events;

using Rabbita.Core;

public class PetAnimalIdChanged : IEvent
{
    public PetAnimalIdChanged(Guid petId, Decimal? animalId, Decimal? oldAnimalId, DateTime date)
    {
        (PetId, AnimalId, OldAnimalId, Date)
            = (petId, animalId, oldAnimalId, date);
    }

    public Guid PetId { get; }
    public Decimal? AnimalId { get; }
    public Decimal? OldAnimalId { get; }
    public DateTime Date { get; }
}