namespace Pets.Domain.News;

using System.Collections.Generic;

using Types.Exceptions;

using ValueTypes;

/// <summary>
///     Доменный сервис создания новости
/// </summary>
public interface INewsCreateService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Title">Заголовок новости</param>
    /// <param name="ImgLink">Ссылка на картинку шапки</param>
    /// <param name="MdShortBody">Предпросмотр новости в markdown</param>
    /// <param name="MdBody">Тело в markdown</param>
    /// <param name="LinkedPets"></param>
    /// <param name="Tags">Теги новости</param>
    public sealed record CreateNews(
        String Title,
        String ImgLink,
        String MdShortBody,
        String MdBody,
        ICollection<Guid> LinkedPets,
        ICollection<String> Tags);

    /// <summary>
    ///     Создать новость
    /// </summary>
    /// <param name="organisationId">Идентификатор организации</param>
    /// <param name="id">Идентификатор новости</param>
    /// <param name="request">Параметры создаваемой новости</param>
    /// <param name="cancellationToken">Токен признака отмены запроса</param>
    /// <exception cref="IdempotencyCheckException"></exception>
    /// <returns></returns>
    Task<Guid> Create(Organisation organisationId, Guid id, CreateNews request, CancellationToken cancellationToken);
}