using System;

using Pets.Types;

namespace Pets.Queries.Organisation
{
    public sealed class ResourceView
    {
        /// <summary>
        /// Ссылка на фотографию материала
        /// </summary>
        public String? ImgLink { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; }

        /// <summary>
        /// Заголовок в markdown
        /// </summary>
        public String? Title { get; }

        /// <summary>
        /// Тип контакта
        /// </summary>
        public ResourceState State { get; }

        public ResourceView(String? imgLink, String? title, String? mdBody, ResourceState state)
            => (ImgLink, Title, MdBody, State)
                = (imgLink, title, mdBody, state);
    }
}