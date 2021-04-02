using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

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
                    commandText: Entity.Search.SearchDto.SqlCounts,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        Filter = $"%{query.Query}%",
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            var items = new List<Search.Entity.Search.SearchDto>(query.Limit);
            var total = query.Offset + query.Limit;

            if (query.Offset < counts.PetCount && counts.PetCount > 0)
            {
                items.AddRange(await _db.QueryAsync<Entity.Search.SearchDto>(new CommandDefinition(
                    commandText: Entity.Search.SearchDto.SqlPets,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        Filter = $"%{query.Query}%",
                        Limit = query.Limit,
                        Offset = query.Offset,
                    }, commandType: CommandType.Text,
                    cancellationToken: cancellationToken)));
            }

            if (total >= counts.PetCount && counts.NewsCount > 0)
            {
                var limit = total - counts.PetCount;
                limit = limit > query.Limit ? query.Limit : limit;
                var offset = query.Offset - counts.PetCount;
                offset = offset > 0 ? offset : 0;
                items.AddRange(await _db.QueryAsync<Entity.Search.SearchDto>(new CommandDefinition(
                    commandText: Entity.Search.SearchDto.SqlNews,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        Filter = $"%{query.Query}%",
                        Limit = limit,
                        Offset = offset,
                    }, commandType: CommandType.Text,
                    cancellationToken: cancellationToken)));
            }

            if (total > counts.PetCount + counts.NewsCount && counts.PageCount > 0)
            {
                var limit = total - (counts.PetCount + counts.NewsCount);
                limit = limit > query.Limit ? query.Limit : limit;
                var offset = query.Offset - (counts.PetCount + counts.NewsCount);
                offset = offset > 0 ? offset : 0;
               
                items.AddRange(await _db.QueryAsync<Entity.Search.SearchDto>(new CommandDefinition(
                    commandText: Entity.Search.SearchDto.SqlPages,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        Filter = $"%{query.Query}%",
                        Limit = limit,
                        Offset = offset,
                    }, commandType: CommandType.Text,
                    cancellationToken: cancellationToken)));
            }

            return new Page<SearchView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = counts.NewsCount + counts.PageCount + counts.PetCount,
                Items = items.Select(_ => new SearchView(_.Id, _.Type, _.Img, _.Title, _.ShortText)),
            };
        }
    }
}