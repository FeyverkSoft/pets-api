namespace Pets.Types.Exceptions;

using System;

/// <summary>
///     Общее исключение не найденного объекта
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(String? message = null, Exception? innerException = null) : base(message, innerException)
    {
    }
}