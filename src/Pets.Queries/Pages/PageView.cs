using System;

namespace Pets.Queries.Pages
{
    public sealed class PageView
    {
        /// <summary>
        /// Id
        /// </summary>
        public String Id { get; }

        /// <summary>
        /// Id организации
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Ссфлка на фотку до
        /// </summary>
        public String ImgLink { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime UpdateDate { get; }

        public PageView(String id, Guid organisationId, String imgLink, String? mdBody, DateTime updateDate)
            => (Id, OrganisationId, ImgLink, MdBody, UpdateDate)
                = (id, organisationId, imgLink, mdBody, updateDate);
    }
}