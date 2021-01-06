using System;

namespace Pets.Queries.News
{
    /// <summary>
    /// Список связанных с новостью животных
    /// </summary>
    public sealed class LinkedPetsView
    {
        /// <summary>
        /// Идентификатор животного
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Имя животного
        /// </summary>
        public String Name { get; }

        public LinkedPetsView(Guid id, String name)
            => (Id, Name)
                = (id, name);
    }
}