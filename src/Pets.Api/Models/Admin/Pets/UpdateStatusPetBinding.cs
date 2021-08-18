using Pets.Types;

namespace Pets.Api.Models.Admin.Pets
{
    public class UpdateStatusPetBinding
    {
        public PetState State { get; set; }
    }
}