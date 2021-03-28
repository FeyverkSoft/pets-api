using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Pets.Queries.Pets;
using Pets.Queries.Search;

using Query.Core;

namespace Pets.Queries.Infrastructure.Search
{
    public sealed class SearchQueryHandler : IQueryHandler<SearchQuery, Page<SearchView>>
    {
        private readonly IDbConnection _db;

        public SearchQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<Page<SearchView>> IQueryHandler<SearchQuery, Page<SearchView>>.Handle(SearchQuery query,
            CancellationToken cancellationToken)
        {
            var counts = await _db.QuerySingleAsync<(Int32 PetCount, Int32 NewsCount, Int32 PageCount)>(
                new CommandDefinition(
                    commandText: Entity.Search.SearchView.SqlCounts,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        Filter = $"%{query.Query}%",
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            var items = new List<SearchView>(query.Limit);
            throw new NotImplementedException("Дописать!");
            return new Page<SearchView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = counts.NewsCount + counts.PageCount + counts.PetCount,
                Items = items,
            };
        }
    }
}