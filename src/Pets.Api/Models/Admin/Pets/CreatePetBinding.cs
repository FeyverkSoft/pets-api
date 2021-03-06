using System;

using Pets.Types;

namespace Pets.Api.Models.Admin.Pets
{
    public sealed class CreatePetBinding
    {
        /// <summary>
        /// Идентификатор питомца
        /// </summary>
        public Guid PetId { get; set; }
        
        /// <summary>
        /// Имя пета
        /// </summary>
        public String Name  { get; set; }

        /// <summary>
        /// Ссфлка на фотку до
        /// </summary>
        public String? BeforePhotoLink  { get; set; }

        /// <summary>
        /// Ссылка на фотку после
        /// </summary>
        public String? AfterPhotoLink  { get; set; }

        /// <summary>
        /// Состояние животного
        /// </summary>
        public PetState PetState  { get; set; }

        /// <summary>
        /// Краткое описание в markdown
        /// </summary>
        public String MdShortBody  { get; set; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody  { get; set; }

        /// <summary>
        /// Pet type
        /// Собака/кот/енот/чупакабра
        /// </summary>
        public PetType Type  { get; set; }
    }
}