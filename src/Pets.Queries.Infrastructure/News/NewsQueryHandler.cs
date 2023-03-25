namespace Pets.Queries.Infrastructure.News;

using System;
using System.Collections.Generic;
using System.Linq;

using Entity;

using Helpers;

using Queries.News;

public sealed class PetsQueryHandler :
    IRequestHandler<GetNewsQuery, Page<NewsView>>,
    IRequestHandler<GetSingleNewsQuery, NewsView?>
{
    private readonly IDbConnection _db;

    public PetsQueryHandler(IDbConnection db)
    {
        _db = db;
    }

    public async Task<Page<NewsView>> Handle(GetNewsQuery query, CancellationToken cancellationToken)
    {
        var result = await _db.QueryMultipleAsync(
            new CommandDefinition(
                NewsDto.Sql,
                new
                {
                    query.OrganisationId,
                    query.PetId,
                    Tag = String.IsNullOrEmpty(query.Tag) ? null : $"%{query.Tag}%",
                    query.Limit,
                    query.Offset,
                    query.NewsId
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        if (result is null)
            return new Page<NewsView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = 0
            };

        return new Page<NewsView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await result.ReadSingleAsync<Int64>(),
            Items = (await result.ReadAsync<NewsDto>()).Select(_ => new NewsView(
                _.Id,
                _.Title,
                _.ImgLink,
                _.MdShortBody,
                _.MdBody,
                _.CreateDate,
                _.LinkedPets?.TryParseJson<List<LinkedPetsDto>>()
                    .Select(lp => new LinkedPetsView(lp.Id, lp.Name)).ToList() ?? new List<LinkedPetsView>(),
                _.Tags?.TryParseJson<List<String>>() ?? new List<String>()
            ))
        };
    }

    public async Task<NewsView?> Handle(GetSingleNewsQuery query, CancellationToken cancellationToken)
    {
        var result = await _db.QuerySingleOrDefaultAsync<NewsDto>(
            new CommandDefinition(
                SingleNewsDto.Sql,
                new
                {
                    query.OrganisationId, query.NewsId
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        if (result is null)
            return null;

        return new NewsView(
            result.Id,
            result.Title,
            result.ImgLink,
            result.MdShortBody,
            result.MdBody,
            result.CreateDate,
            result.LinkedPets?.TryParseJson<List<LinkedPetsDto>>()
                .Select(lp => new LinkedPetsView(lp.Id, lp.Name)).ToList() ?? new List<LinkedPetsView>(),
            result.Tags?.TryParseJson<List<String>>() ?? new List<String>()
        );
    }
}