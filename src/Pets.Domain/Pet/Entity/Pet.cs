using System;
using System.Collections.Generic;

using Pets.Domain.Pet.Events;
using Pets.Types;

using Rabbita.Core;

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
        public String? BeforePhotoLink { get; set; }

        /// <summary>
        /// Ссылка на фотку после
        /// </summary>
        public String? AfterPhotoLink { get; set; }

        /// <summary>
        /// Состояние животного
        /// </summary>
        public PetState PetState { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdShortBody { get; set; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; set; }

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

        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; } = Guid.NewGuid();

        public List<IEvent> Events { get; } = new();

#pragma warning disable 8618 FOR EF CORE
        internal Pet()
        {
        }
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

        /// <summary>
        /// Обновить фотографию профайла пета
        /// </summary>
        /// <param name="beforePhotoLink"></param>
        /// <param name="afterPhotoLink"></param>
        /// <param name="date"></param>
        public void UpdateImg(String? beforePhotoLink, String? afterPhotoLink, DateTime date)
        {
            if (String.IsNullOrEmpty(beforePhotoLink?.Trim()) && String.IsNullOrEmpty(afterPhotoLink?.Trim()))
                return;
            if (BeforePhotoLink == beforePhotoLink && AfterPhotoLink == afterPhotoLink)
                return;

            var before = beforePhotoLink ?? afterPhotoLink;

            if (before is not null && !before.Equals(BeforePhotoLink, StringComparison.InvariantCultureIgnoreCase))
            {
                Events.Add(new PetChangeImg(petId: Id, link: before, date: date));
            }

            if (afterPhotoLink is not null && !afterPhotoLink.Equals(AfterPhotoLink, StringComparison.InvariantCultureIgnoreCase))
            {
                Events.Add(new PetChangeImg(petId: Id, link: afterPhotoLink, date: date));
            }

            AfterPhotoLink = afterPhotoLink;
            BeforePhotoLink = before;
            UpdateDate = date;
        }

        /// <summary>
        /// Обновить описание и тексты в инфе о пете
        /// </summary>
        /// <param name="mdShortBody"></param>
        /// <param name="mdBody"></param>
        /// <param name="date"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateDescription(String? mdShortBody, String? mdBody, DateTime date)
        {
            if ((MdShortBody is not null && MdShortBody.Equals(mdShortBody))
                && (MdBody is not null && MdBody.Equals(mdBody)))
                return;

            if (String.IsNullOrEmpty(mdShortBody?.Trim()) && String.IsNullOrEmpty(mdBody?.Trim()))
                return;

            if (!String.IsNullOrEmpty(mdShortBody?.Trim()))
            {
                MdShortBody = mdShortBody;
                MdBody ??= mdShortBody;
            }

            if (!String.IsNullOrEmpty(mdBody?.Trim()) && !mdBody.Equals(MdBody))
            {
                MdBody = mdBody;
                Events.Add(new PetChangeDescription(petId: Id, body: mdBody, date: date));
            }

            UpdateDate = date;
        }
    }
}