using System;

using Pets.Types;

namespace Pets.DB.Migrations.Entities
{
    sealed class Resource
    {
        public String Id { get; }

        /// <summary>
        /// Идентификатор организации которой принадлежит контакт
        /// </summary>
        public Guid OrganisationId { get; }

        public Organisation Organisation { get; }

        /// <summary>
        /// Название ресурса
        /// </summary>
        public String Title { get; }

        /// <summary>
        /// Ссылка на картинки
        /// JSON
        /// </summary>
        public String ImgLink { get; }

        /// <summary>
        /// Markdown текст описание
        /// </summary>
        public String MdBody { get; }

        /// <summary>
        /// Состояние ресурса
        /// </summary>
        public ResourceState State { get; }

        public Guid ConcurrencyTokens { get; }
    }
}