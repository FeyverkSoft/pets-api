using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Pets.Helpers;
using Pets.Queries.Infrastructure.News.Entity;
using Pets.Queries.News;

using Query.Core;

namespace Pets.Queries.Infrastructure.News
{
    public sealed class PetsQueryHandler :
        IQueryHandler<GetNewsQuery, Page<NewsView>>,
        IQueryHandler<GetSingleNewsQuery, NewsView?>
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
                    commandText: Entity.NewsDto.Sql,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        PetId = query.PetId,
                        Tag = String.IsNullOrEmpty(query.Tag) ? null : $"%{query.Tag}%",
                        Limit = query.Limit,
                        Offset = query.Offset,
                        NewsId = query.NewsId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            if (result is null)
                return new Page<NewsView>
                {
                    Limit = query.Limit,
                    Offset = query.Offset,
                    Total = 0,
                };

            return new Page<NewsView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await result.ReadSingleAsync<Int64>(),
                Items = (await result.ReadAsync<Entity.NewsDto>()).Select(_ => new NewsView(
                    id: _.Id,
                    title: _.Title,
                    imgLink: _.ImgLink,
                    mdShortBody: _.MdShortBody,
                    mdBody: _.MdBody,
                    createDate: _.CreateDate,
                    linkedPets: _.LinkedPets?.TryParseJson<List<LinkedPetsDto>>()
                        .Select(lp => new LinkedPetsView(lp.Id, lp.Name)).ToList() ?? new List<LinkedPetsView>(),
                    tags: _.Tags?.TryParseJson<List<String>>() ?? new List<String>()
                ))
            };
        }

        public async Task<NewsView?> Handle(GetSingleNewsQuery query, CancellationToken cancellationToken)
        {
            var result = await _db.QuerySingleOrDefaultAsync<Entity.NewsDto>(
                new CommandDefinition(
                    commandText: Entity.SingleNewsDto.Sql,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        NewsId = query.NewsId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            if (result is null)
                return null;

            return new NewsView(
                id: result.Id,
                title: result.Title,
                imgLink: result.ImgLink,
                mdShortBody: result.MdShortBody,
                mdBody: result.MdBody,
                createDate: result.CreateDate,
                linkedPets: result.LinkedPets?.TryParseJson<List<LinkedPetsDto>>()
                    .Select(lp => new LinkedPetsView(lp.Id, lp.Name)).ToList() ?? new List<LinkedPetsView>(),
                tags: result.Tags?.TryParseJson<List<String>>() ?? new List<String>()
            );
        }
    }
}