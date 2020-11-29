using System;

namespace Pets.DB.Migrations.Entities
{
    /// <summary>
    /// Служебная таблица для связи многие ко многим
    /// Много новостей много животных
    /// </summary>
    sealed class NewsPets
    {
        /// <summary>
        /// Идентификатор животного
        /// </summary>
        public Guid PetId { get; }
        public Pet? Pet { get; }
        
        /// <summary>
        /// Идентификатор новости
        /// </summary>
        public Guid NewsId { get; }
        public News? News { get; }
    }
}