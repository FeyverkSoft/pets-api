using System;

namespace Pets.Domain.Pet.Entity
{
    public sealed class Organisation
    {
        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public Guid Id { get; }
        
        private Organisation() { }
        public Organisation(Guid id)
        {
            Id = id;
        }
        
        public static implicit operator Guid(Organisation organisation) => organisation.Id;
        public static explicit operator Organisation(Guid id) => new (id);
    }
}