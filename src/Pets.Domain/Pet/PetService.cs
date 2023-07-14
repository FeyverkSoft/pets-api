namespace Pets.Domain.Pet;

using Core;

using Entity;

using Exceptions;

using Specs;

using Types;
using Types.Exceptions;

public sealed class PetService :
    IPetCreateService,
    IPetUpdateService
{
    private readonly IDateTimeGetter _dateTimeGetter;
    private readonly IPetRepository _petRepository;

    public PetService(
        IPetRepository petRepository,
        IDateTimeGetter dateTimeGetter
    )
    {
        _petRepository = petRepository;
        _dateTimeGetter = dateTimeGetter;
    }

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
    public async Task<Guid> Create(
        Guid petId,
        Guid organisationId,
        String name,
        PetGender gender,
        PetType type,
        PetState petState,
        String? afterPhotoLink,
        String? beforePhotoLink,
        String? mdShortBody,
        String? mdBody,
        Decimal? animalId,
        CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetAsync(PetSpecs.IsSatisfiedById(petId, (Organisation)organisationId), cancellationToken);
        
        // если с таким id пет уже был найден, то выполняем проверку идемпотентности
        if (pet is not null)
        {
            if (pet.Gender != gender ||
                pet.Type != type ||
                pet.PetState != petState ||
                pet.AnimalId != animalId ||
                !name.Equals(pet.Name, StringComparison.InvariantCultureIgnoreCase) ||
                (mdShortBody is not null && !mdShortBody.Equals(pet.MdShortBody, StringComparison.InvariantCultureIgnoreCase)) ||
                (mdBody is not null && !mdBody.Equals(pet.MdBody, StringComparison.InvariantCultureIgnoreCase))
               )
                throw new PetAlreadyExistsException(petId);

            return pet.Id;
        }

        await _petRepository.SaveAsync(
            new Pet(
                petId,
                new Organisation(organisationId),
                name,
                gender,
                type,
                petState,
                afterPhotoLink,
                beforePhotoLink,
                mdShortBody,
                mdBody,
                animalId: animalId,
                createDate: _dateTimeGetter.Get(),
                updateDate: _dateTimeGetter.Get()),
            cancellationToken
        );
        return petId;
    }

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
    public async Task Update(
        Guid petId,
        Guid organisationId,
        String? afterPhotoLink,
        String? beforePhotoLink,
        String? mdShortBody,
        String? mdBody,
        CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetAsync(PetSpecs.IsSatisfiedById(petId, (Organisation)organisationId), cancellationToken);

        if (pet is null)
            throw new PetNotFoundException(petId, organisationId);

        pet.UpdateDescription(mdShortBody, mdBody, _dateTimeGetter.Get());
        pet.UpdateImg(beforePhotoLink, afterPhotoLink, _dateTimeGetter.Get());

        await _petRepository.SaveAsync(
            pet,
            cancellationToken
        );
    }

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
    public async Task UpdateName(Guid petId, Guid organisationId, String name, String reason, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetAsync(PetSpecs.IsSatisfiedById(petId, (Organisation)organisationId), cancellationToken);

        if (pet is null)
            throw new PetNotFoundException(petId, organisationId);

        pet.ChangePetName(
            name,
            reason,
            _dateTimeGetter.Get()
        );

        await _petRepository.SaveAsync(
            pet,
            cancellationToken
        );
    }

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
    public async Task SetGender(Guid petId, Guid organisationId, PetGender gender, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetAsync(PetSpecs.IsSatisfiedById(petId, (Organisation)organisationId), cancellationToken);

        if (pet is null)
            throw new PetNotFoundException(petId, organisationId);

        pet.ChangePetGender(
            gender,
            _dateTimeGetter.Get()
        );

        await _petRepository.SaveAsync(
            pet,
            cancellationToken
        );
    }

    /// <summary>
    ///     Изменить статус питомца
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="organisationId"></param>
    /// <param name="gender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SetStatus(Guid petId, Guid organisationId, PetState state, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetAsync(PetSpecs.IsSatisfiedById(petId, (Organisation)organisationId), cancellationToken);

        if (pet is null)
            throw new PetNotFoundException(petId, organisationId);

        pet.ChangePetStatus(
            state,
            _dateTimeGetter.Get()
        );

        await _petRepository.SaveAsync(
            pet,
            cancellationToken
        );
    }

    /// <summary>
    /// Изменить тип питомца
    /// </summary>
    /// <returns></returns>
    public async Task SetType(Guid petId, Guid organisationId, PetType type, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetAsync(PetSpecs.IsSatisfiedById(petId, (Organisation)organisationId), cancellationToken);

        if (pet is null)
            throw new PetNotFoundException(petId, organisationId);

        pet.ChangePetType(
            type,
            _dateTimeGetter.Get()
        );

        await _petRepository.SaveAsync(
            pet,
            cancellationToken
        );
    }
}