namespace Pets.Types.Exceptions;

using System;

/// <summary>
///     Общее исключение для проверок идемпотентности
/// </summary>
public class IdempotencyCheckException : Exception
{
    public IdempotencyCheckException((String? Name, Guid Id) message, Exception? innerException = null) : base($"{message.Name} with id: {message.Id} and not matching data already exists;", innerException)
    {
    }
}