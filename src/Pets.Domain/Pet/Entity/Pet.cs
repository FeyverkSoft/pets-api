using System;

using Pets.Types;

namespace Pets.Domain.Pet.Entity
{
    /// <summary>
    /// Информация о животном
    /// </summary>
    public sealed class Pet
    {
        public Guid Id { get; }

        /// <summary>
        /// Организация которой принадлежит животное
        /// </summary>
        public Organisation Organisation { get; }

        /// <summary>
        /// Имя животного
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Ссфлка на фотку до
        /// </summary>
        public String? BeforePhotoLink { get; }

        /// <summary>
        /// Ссылка на фотку после
        /// </summary>
        public String? AfterPhotoLink { get; }

        /// <summary>
        /// Состояние животного
        /// </summary>
        public PetState PetState { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdShortBody { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; }

        /// <summary>
        /// Pet type
        /// Собака/кот/енот/чупакабра
        /// </summary>
        public PetType Type { get; }

        /// <summary>
        /// Pet type
        /// Мальчик/Девочка/Неизвестно
        /// </summary>
        public PetGender Gender { get; }

        public DateTime UpdateDate { get; } = DateTime.UtcNow;
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; } = Guid.NewGuid();

#pragma warning disable 8618 FOR EF CORE
        internal Pet() { }
#pragma warning restore 8618
        
        public Pet(
            Guid petId,
            Organisation organisation,
            String name,
            PetGender gender,
            PetType type,
            PetState petState,
            String? afterPhotoLink,
            String? beforePhotoLink,
            String? mdShortBody,
            String? mdBody,
            DateTime createDate,
            DateTime updateDate
        )
        {
            Id = petId;
            Name = name;
            Gender = gender;
            Type = type;
            PetState = petState;
            AfterPhotoLink = afterPhotoLink;
            BeforePhotoLink = beforePhotoLink;
            MdBody = mdBody ?? String.Empty;
            MdShortBody = mdShortBody ?? String.Empty;
            Organisation = organisation;
            CreateDate = createDate;
            UpdateDate = updateDate;
        }
    }
}