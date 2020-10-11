using System;

namespace Pets.DB.Migrations.Entities
{
    sealed class Organisation
    {
        /// <summary>
        /// Идентификатор организации которой принадлежит животное
        /// </summary>
        public Guid Id { get; }
    }
}