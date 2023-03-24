namespace Pets.Domain.Pet;

using Exceptions;

using Types;
using Types.Exceptions;

/// <summary>
///     Доменный сервис изменения питомцев
/// </summary>
public interface IPetUpdateService
{
    /// <summary>
    ///     Обновить информацию о питомце
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="organisationId"></param>
    /// <param name="afterPhotoLink"></param>
    /// <param name="beforePhotoLink"></param>
    /// <param name="mdShortBody"></param>
    /// <param name="mdBody"></param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="PetNotFoundException"></exception>
    /// <param name="cancellationToken">Токен признака отмены запроса</param>
    /// <returns></returns>
    Task Update(Guid petId, Guid organisationId, String? afterPhotoLink, String? beforePhotoLink, String? mdShortBody,
        String? mdBody, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить имя питомцу
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="organisationId"></param>
    /// <param name="name">Новое имя питомца</param>
    /// <param name="reason">Причина изменения имени</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="PetNotFoundException"></exception>
    /// <returns></returns>
    Task UpdateName(Guid petId, Guid organisationId, String name, String reason, CancellationToken cancellationToken);

    /// <summary>
    ///     Изменить пол у питомца
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="organisationId"></param>
    /// <param name="gender">Новый пол питомца</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="PetNotFoundException"></exception>
    /// <returns></returns>
    Task SetGender(Guid petId, Guid organisationId, PetGender gender, CancellationToken cancellationToken);

    /// <summary>
    ///     Изменить статус питомца
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="organisationId"></param>
    /// <param name="state"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetStatus(Guid petId, Guid organisationId, PetState state, CancellationToken cancellationToken);
}