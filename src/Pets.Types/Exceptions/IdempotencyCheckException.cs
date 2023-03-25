namespace Pets.Types.Exceptions;

using System;

/// <summary>
///     Общее исключение для проверок идемпотентности
/// </summary>
public class IdempotencyCheckException : Exception
{
    public IdempotencyCheckException(String? message = null, Exception? innerException = null) : base(message, innerException)
    {
    }
}