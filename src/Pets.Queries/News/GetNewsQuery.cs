﻿using System;

namespace Pets.Queries.News
{
    public sealed class GetNewsQuery : PageQuery<NewsView>
    {
        /// <summary>
        /// Организация к которой относятся новости
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Идентификатор животного по которому выбираются новости
        /// </summary>
        public Guid? PetId { get; }

        /// <summary>
        /// Список тегов по которым выбираются новости
        /// </summary>
        public String? Tags { get; }

        /// <summary>
        /// идентификатор конкретной новости
        /// </summary>
        public Guid? NewsId { get; }

        public GetNewsQuery(Guid organisationId, Int32 offset = 0, Int32 limit = 8, Guid? petId = null, String? tags = null, Guid? newsId = null)
            : base(offset, limit)
            => (OrganisationId, PetId, Tags, NewsId)
                = (organisationId, petId, tags, newsId);
    }
}