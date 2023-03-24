namespace Pets.Domain.Pet.Specs;

using Core;

using Entity;

using Types;

public static class PetSpecs
{
    public static readonly Specification<Pet> IsFemale = new(x => x.Gender == PetGender.Female);

    public static readonly Specification<Pet> IsAlive = new(x => x.PetState == PetState.Alive ||
                                                                 x.PetState == PetState.Adopted ||
                                                                 x.PetState == PetState.OurPets ||
                                                                 x.PetState == PetState.Critical ||
                                                                 x.PetState == PetState.Wanted);
}