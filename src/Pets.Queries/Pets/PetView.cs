using System;

using Pets.Types;

namespace Pets.Queries.Pets
{
    /// <summary>
    /// Инфа о петомце
    /// </summary>
    public class PetView
    {
        public Guid Id { get; }

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
        /// Краткое описание в markdown
        /// </summary>
        public String MdShortBody { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; }

        /// <summary>
        /// Pet type
        /// Собака/кот/енот/чупакабра
        /// </summary>
        public PetType Type { get; }

        public DateTime UpdateDate { get; }

        public PetView(Guid id, String name, String? beforePhotoLink, String? afterPhotoLink, PetState petState, String mdShortBody, String? mdBody,
            PetType type, DateTime updateDate)
            => (Id, Name, BeforePhotoLink, AfterPhotoLink, PetState, MdShortBody, MdBody, Type, UpdateDate)
                = (id, name, beforePhotoLink, afterPhotoLink, petState, mdShortBody, mdBody, type, updateDate);
    }
}