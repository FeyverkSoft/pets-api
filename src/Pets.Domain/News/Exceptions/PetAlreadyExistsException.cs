namespace Pets.Domain.News.Exceptions;

using Types.Exceptions;

/// <summary>
///     Новость уже существует но с отличающимся набором полей, проверка идемпотентности не прошла
/// </summary>
public sealed class NewsAlreadyExistsException : IdempotencyCheckException
{
    public NewsAlreadyExistsException(Guid id) : base(("News", id))
    {
    }
}