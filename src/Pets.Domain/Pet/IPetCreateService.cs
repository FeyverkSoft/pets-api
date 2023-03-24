namespace Pets.Domain.Pet;

using Exceptions;

using Types;
using Types.Exceptions;

/// <summary>
///     Доменный сервис создания питомцев
/// </summary>
public interface IPetCreateService
{
    /// <summary>
    ///     Создать питомца
    /// </summary>
    /// <param name="petId">Идентификатор питомца</param>
    /// <param name="organisationId">Идентификатор организации</param>
    /// <param name="name">Имя питомца</param>
    /// <param name="gender">Пол питомца</param>
    /// <param name="type">Тип питомца</param>
    /// <param name="petState">Статус питомца</param>
    /// <param name="afterPhotoLink">Ссылка на фотку после</param>
    /// <param name="beforePhotoLink">Ссылка на фотку До</param>
    /// <param name="mdShortBody">Краткий текст</param>
    /// <param name="mdBody">Длинный текст</param>
    /// <param name="animalId">Уникальный идентификатор петомца в Animal-Id</param>
    /// <param name="cancellationToken">Токен признака отмены запроса</param>
    /// <exception cref="PetAlreadyExistsException"></exception>
    /// <exception cref="IdempotencyCheckException"></exception>
    /// <returns></returns>
    Task<Guid> Create(Guid petId, Guid organisationId, String name, PetGender gender, PetType type, PetState petState, String? afterPhotoLink,
        String? beforePhotoLink, String? mdShortBody, String? mdBody, Decimal? animalId, CancellationToken cancellationToken);
}