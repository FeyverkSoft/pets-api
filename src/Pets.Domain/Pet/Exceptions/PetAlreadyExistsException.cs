namespace Pets.Domain.Pet.Exceptions;

using Types.Exceptions;

/// <summary>
///     Питомец уже существует но с отличающимся набором полей, проверка идемпотентности не прошла
/// </summary>
public sealed class PetAlreadyExistsException : IdempotencyCheckException
{
    public PetAlreadyExistsException(Guid petId) : base(("Pet", petId))
    {
    }
}