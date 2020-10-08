using System;

namespace Pets.Queries.Pets
{
    /// <summary>
    /// Инфа о петомце
    /// </summary>
    public class PetView
    {
        /// <summary>
        /// Character id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Класс персонажа
        /// </summary>
        public String ClassName { get; }

        /// <summary>
        /// Признак основного персонажа
        /// </summary>
        public Boolean IsMain { get; }
        public Guid UserId { get; }

        public PetView(Guid id, String name, String className, Boolean isMain, Guid userId)
                => (Id, Name, ClassName, IsMain, UserId)
                = (id, name, className, isMain, userId);
    }
}