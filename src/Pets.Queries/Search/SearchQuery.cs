﻿using System;

namespace Pets.Queries.Search
{
    public sealed class SearchQuery : PageQuery<SearchView>
    {
        /// <summary>
        /// Организация в которой выполняется поиск
        /// </summary>
        public Guid OrganisationId { get; }
        
        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public String Query { get; }
        
        public SearchQuery(Guid organisationId, String query, Int32 offset = 0, Int32 limit = 8)
            : base(offset, limit)
            => (OrganisationId, Query)
                = (organisationId, query);
    }
}