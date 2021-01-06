﻿using System;
using System.Collections.Generic;

namespace Pets.Queries.News
{
    public sealed class NewsView
    {
        /// <summary>
        /// Идентификатор новости
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Теги новости
        /// </summary>
        public ICollection<String> Tags { get; }

        /// <summary>
        /// Ссылка на картинку шапки
        /// </summary>
        public String ImgLink { get; }

        /// <summary>
        /// Предпросмотр новости в markdown
        /// </summary>
        public String MdShortBody { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String MdBody { get; }

        /// <summary>
        /// Дата публикации новости
        /// </summary>
        public DateTime CreateDate { get; }

        public ICollection<LinkedPetsView> LinkedPets { get; }

        public NewsView(Guid id, String imgLink, String mdShortBody, String mdBody, DateTime createDate, ICollection<LinkedPetsView> linkedPets,
            ICollection<String> tags)
            => (Id, ImgLink, MdShortBody, MdBody, CreateDate, LinkedPets, Tags)
                = (id, imgLink, mdShortBody, mdBody, createDate, linkedPets, tags);
    }
}