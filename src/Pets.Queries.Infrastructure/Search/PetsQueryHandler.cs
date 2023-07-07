namespace Pets.Queries.Infrastructure.Search;

using System;
using System.Collections.Generic;
using System.Linq;

using Entity.Search;

using Queries.Search;

public sealed class SearchQueryHandler : IRequestHandler<SearchQuery, Page<SearchView>>
{
    private readonly IDbConnection _db;

    public SearchQueryHandler(IDbConnection db)
    {
        _db = db;
    }

    async Task<Page<SearchView>> IRequestHandler<SearchQuery, Page<SearchView>>.Handle(SearchQuery query,
        CancellationToken cancellationToken)
    {
        var counts = await _db.QuerySingleAsync<(Int32 PetCount, Int32 NewsCount, Int32 PageCount)>(
            new CommandDefinition(
                SearchDto.SqlCounts,
                new
                {
                    query.OrganisationId,
                    Filter = $"%{query.Query}%"
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));

        var items = new List<SearchDto>(query.Limit);
        var total = query.Offset + query.Limit;

        if (query.Offset < counts.PetCount && counts.PetCount > 0)
            items.AddRange(await _db.QueryAsync<SearchDto>(new CommandDefinition(
                SearchDto.SqlPets,
                new
                {
                    query.OrganisationId,
                    Filter = $"%{query.Query}%",
                    query.Limit,
                    query.Offset
                }, commandType: CommandType.Text,
                cancellationToken: cancellationToken)));

        if (total >= counts.PetCount && counts.NewsCount > 0)
        {
            var limit = total - counts.PetCount;
            limit = limit > query.Limit ? query.Limit : limit;
            var offset = query.Offset - counts.PetCount;
            offset = offset > 0 ? offset : 0;
            items.AddRange(await _db.QueryAsync<SearchDto>(new CommandDefinition(
                SearchDto.SqlNews,
                new
                {
                    query.OrganisationId,
                    Filter = $"%{query.Query}%",
                    Limit = limit,
                    Offset = offset
                }, commandType: CommandType.Text,
                cancellationToken: cancellationToken)));
        }

        if (total > counts.PetCount + counts.NewsCount && counts.PageCount > 0)
        {
            var limit = total - (counts.PetCount + counts.NewsCount);
            limit = limit > query.Limit ? query.Limit : limit;
            var offset = query.Offset - (counts.PetCount + counts.NewsCount);
            offset = offset > 0 ? offset : 0;

            items.AddRange(await _db.QueryAsync<SearchDto>(new CommandDefinition(
                SearchDto.SqlPages,
                new
                {
                    query.OrganisationId,
                    Filter = $"%{query.Query}%",
                    Limit = limit,
                    Offset = offset
                }, commandType: CommandType.Text,
                cancellationToken: cancellationToken)));
        }

        return new Page<SearchView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = counts.NewsCount + counts.PageCount + counts.PetCount,
            Items = items.Select(_ => new SearchView(
                Id: _.Id,
                Type: _.Type,
                Img: _.Img,
                Title: _.Title,
                ShortText: _.ShortText))
        };
    }
}