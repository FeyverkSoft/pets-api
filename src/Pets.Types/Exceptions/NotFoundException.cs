using System;

namespace Pets.Types.Exceptions
{
    /// <summary>
    /// Общее исключение не найденного объекта
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(String? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}