using System;

using Rabbita.Core;

namespace Pets.Domain.Pet.Events
{
    /// <summary>
    /// Cобытие изменения фотки животного в профайле
    /// </summary>
    public sealed class PetChangeImg : IEvent
    {
        /// <summary>
        /// Идентификатор питомца
        /// </summary>
        public Guid PetId { get; }

        /// <summary>
        /// Дата события
        /// </summary>
        public DateTime Date { get; }
        
        /// <summary>
        /// Ссылка на новую фотку
        /// </summary>
        public String Link { get; }

        protected PetChangeImg() { }
        public PetChangeImg(Guid petId, String link, DateTime date)
        {
            PetId = petId;
            Date = date;
            Link = link;
        }
    }
}