namespace Pets.Domain.Pet.Specs;

using Core;

using Entity;

using Types;

public static class PetSpecs
{
    public static Specification<Pet> IsSatisfiedById(Guid id, Organisation organisation) => new(x => x.Id == id && x.Organisation.Id == organisation);

    public static Specification<Pet> IsIdempotence(Pet pet) => new(
        x => x.Gender != pet.Gender ||
             x.Type != pet.Type ||
             x.PetState != pet.PetState ||
             x.AnimalId != pet.AnimalId ||
             !x.Name.Equals(pet.Name, StringComparison.InvariantCultureIgnoreCase) ||
             (x.MdShortBody != null && !x.MdShortBody.Equals(pet.MdShortBody, StringComparison.InvariantCultureIgnoreCase)) ||
             (x.MdBody != null && !x.MdBody.Equals(pet.MdBody, StringComparison.InvariantCultureIgnoreCase)));

    public static readonly Specification<Pet> IsFemale = new(x => x.Gender == PetGender.Female);

    public static readonly Specification<Pet> IsAlive = new(x => x.PetState == PetState.Alive ||
                                                                 x.PetState == PetState.Adopted ||
                                                                 x.PetState == PetState.OurPets ||
                                                                 x.PetState == PetState.Critical ||
                                                                 x.PetState == PetState.Wanted);
}