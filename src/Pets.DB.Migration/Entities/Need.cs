using System;

namespace Pets.DB.Migrations.Entities
{
    sealed class Need
    {
        public String Id { get; }

        /// <summary>
        /// Идентификатор организации которой принадлежит контакт
        /// </summary>
        public Guid OrganisationId { get; }

        public Organisation Organisation { get; }

        /// <summary>
        /// Ссылка на картинки
        /// JSON
        /// </summary>
        public String ImgLinks { get; }

        /// <summary>
        /// Markdown текст описание
        /// </summary>
        public String MdBody { get; }

        public Guid ConcurrencyTokens { get; }
    }
}