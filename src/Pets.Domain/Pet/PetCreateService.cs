using System;
using System.Threading;
using System.Threading.Tasks;

using Core;

using Pets.Domain.Pet.Entity;
using Pets.Domain.Pet.Exceptions;
using Pets.Types;
using Pets.Types.Exceptions;

namespace Pets.Domain.Pet
{
    public sealed class PetCreateService : IPetCreateService
    {
        private readonly IPetRepository _petRepository;
        private readonly IDateTimeGetter _dateTimeGetter;

        public PetCreateService(
            IPetRepository petRepository,
            IDateTimeGetter dateTimeGetter
        )
        {
            _petRepository = petRepository;
            _dateTimeGetter = dateTimeGetter;
        }

        /// <summary>
        /// Создать питомца
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
            CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetAsync(petId: petId, organisation: (Organisation) organisationId, cancellationToken: cancellationToken);
            // если с таким id пет уже был найден, то выполняем проверку идемпотентности
            if (pet is not null)
            {
                if (pet.Gender != gender ||
                    pet.Type != type ||
                    pet.PetState != petState ||
                    !name.Equals(pet.Name, StringComparison.InvariantCultureIgnoreCase) ||
                    (mdShortBody is not null && !mdShortBody.Equals(pet.MdShortBody, StringComparison.InvariantCultureIgnoreCase)) ||
                    (mdBody is not null && !mdBody.Equals(pet.MdBody, StringComparison.InvariantCultureIgnoreCase))
                )
                    throw new PetAlreadyExistsException(petId);
                
                return pet.Id;
            }

            await _petRepository.SaveAsync(
                pet: new Entity.Pet(
                    petId: petId,
                    organisation: new Organisation(organisationId),
                    name: name,
                    gender: gender,
                    type: type,
                    petState: petState,
                    afterPhotoLink: afterPhotoLink,
                    beforePhotoLink: beforePhotoLink,
                    mdShortBody: mdShortBody,
                    mdBody: mdBody,
                    createDate: _dateTimeGetter.Get(),
                    updateDate: _dateTimeGetter.Get()),
                cancellationToken: cancellationToken
            );
            return petId;
        }
    }
}