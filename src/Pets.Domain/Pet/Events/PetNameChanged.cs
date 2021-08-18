using System;

using Rabbita.Core;

namespace Pets.Domain.Pet.Events
{
    /// <summary>
    /// Уведомление об изменении имени питомца
    /// </summary>
    public class PetNameChanged : IEvent
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
        /// Новое имя пета
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Старое имя пета
        /// </summary>
        public String OldName { get; }

        /// <summary>
        /// Причина смены имени
        /// </summary>
        public String Reason { get; }

        protected PetNameChanged()
        {
        }

        public PetNameChanged(Guid petId, String name, String oldName, String reason, DateTime date)
        {
            PetId = petId;
            Date = date;
            Name = name;
            OldName = oldName;
            Reason = reason;
        }
    }
}