using System;

using Pets.Types;

namespace Pets.Queries.Search
{
    /// <summary>
    /// Сводка о найденном объекте
    /// </summary>
    public sealed class SearchView
    {
        /// <summary>
        /// идентификатор найденного объекта
        /// </summary>
        public String Id { get; }

        /// <summary>
        /// Тип найденного объекта.
        /// </summary>
        public SearchObjectType Type { get; }

        /// <summary>
        /// Картинка превью к объекту
        /// </summary>
        public String Img { get; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public String Title { get; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public String ShortText { get; }
        
        public SearchView(String id, SearchObjectType type, String img, String title, String shortText)
            => (Id, Type, Img, Title, ShortText)
                = (id, type, img, title, shortText);
    }
}