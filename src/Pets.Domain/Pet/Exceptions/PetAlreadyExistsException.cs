using System;

using Pets.Types.Exceptions;

namespace Pets.Domain.Pet.Exceptions
{
    /// <summary>
    /// Питомец уже существует но с отличающимся набором полей, проверка идемпотентности не прошла
    /// </summary>
    public sealed class PetAlreadyExistsException : IdempotencyCheckException
    {
        public PetAlreadyExistsException(Guid petId) : base($"Pet with id: {petId} and not matching data already exists;")
        {
        }
    }
}