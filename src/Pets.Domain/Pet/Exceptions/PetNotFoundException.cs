using System;

using Pets.Types.Exceptions;

namespace Pets.Domain.Pet.Exceptions
{
    /// <summary>
    /// Питомец с указанным идентификатором не был найден у данной организации
    /// </summary>
    public sealed class PetNotFoundException : NotFoundException
    {
        public PetNotFoundException(Guid petId, Guid organisationId) : base($"Pet with id: {petId} and organisation id: {organisationId} not found;")
        {
        }
    }
}