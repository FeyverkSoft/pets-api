﻿namespace Pets.Queries.Infrastructure.Organisation;

using System;
using System.Collections.Generic;
using System.Linq;

using Helpers;

using Queries.Organisation;

using Types;

public sealed class DocumentsQueryHandler :
    IRequestHandler<GetContactsQuery, IEnumerable<ContactView>>,
    IRequestHandler<GetBuildingQuery, IEnumerable<ResourceView>>,
    IRequestHandler<GetNeedQuery, IEnumerable<NeedView>>
{
    private readonly IDbConnection _db;

    public DocumentsQueryHandler(IDbConnection db)
    {
        _db = db;
    }

    async Task<IEnumerable<ResourceView>> IRequestHandler<GetBuildingQuery, IEnumerable<ResourceView>>.Handle(GetBuildingQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _db.QueryAsync<Entity.ResourceView>(
            new CommandDefinition(
                Entity.ResourceView.Sql,
                new
                {
                    query.OrganisationId
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        return result.Select(_ => new ResourceView(
            ImgLink: _.ImgLink,
            MdBody: _.MdBody,
            Title: _.Title,
            State: _.State
        ));
    }

    async Task<IEnumerable<ContactView>> IRequestHandler<GetContactsQuery, IEnumerable<ContactView>>.Handle(GetContactsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _db.QueryAsync<Entity.ContactView>(
            new CommandDefinition(
                Entity.ContactView.Sql,
                new
                {
                    query.OrganisationId
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        return result.Select(_ => new ContactView(
            ImgLink: _.ImgLink,
            MdBody: _.MdBody,
            ContactType: _.Type
        ));
    }

    async Task<IEnumerable<NeedView>> IRequestHandler<GetNeedQuery, IEnumerable<NeedView>>.Handle(GetNeedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _db.QueryAsync<Entity.NeedView>(
            new CommandDefinition(
                Entity.NeedView.Sql,
                new
                {
                    query.OrganisationId,
                    State = new[] { NeedState.Active }.Select(_ => _.ToString())
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        return result.Select(_ => new NeedView(
            ImgsLink: _.ImgLinks?.ParseJson<IEnumerable<String?>>() ?? new String[] { },
            MdBody: _.MdBody,
            State: _.State
        ));
    }
}