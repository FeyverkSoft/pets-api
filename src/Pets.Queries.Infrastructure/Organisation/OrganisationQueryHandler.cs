using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Pets.Helpers;
using Pets.Queries.Organisation;
using Pets.Types;

using Query.Core;

namespace Pets.Queries.Infrastructure.Organisation
{
    public sealed class DocumentsQueryHandler :
        IQueryHandler<GetContactsQuery, IEnumerable<ContactView>>,
        IQueryHandler<GetBuildingQuery, IEnumerable<ResourceView>>,
        IQueryHandler<GetNeedQuery, IEnumerable<NeedView>>
    {
        private readonly IDbConnection _db;

        public DocumentsQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<IEnumerable<ContactView>> IQueryHandler<GetContactsQuery, IEnumerable<ContactView>>.Handle(GetContactsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _db.QueryAsync<Entity.ContactView>(
                new CommandDefinition(
                    commandText: Entity.ContactView.Sql,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return result.Select(_ => new ContactView(
                imgLink: _.ImgLink,
                mdBody: _.MdBody,
                contactType: _.Type
            ));
        }

        async Task<IEnumerable<ResourceView>> IQueryHandler<GetBuildingQuery, IEnumerable<ResourceView>>.Handle(GetBuildingQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _db.QueryAsync<Entity.ResourceView>(
                new CommandDefinition(
                    commandText: Entity.ResourceView.Sql,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return result.Select(_ => new ResourceView(
                imgLink: _.ImgLink,
                mdBody: _.MdBody,
                title: _.Title,
                state: _.State
            ));
        }

        async Task<IEnumerable<NeedView>> IQueryHandler<GetNeedQuery, IEnumerable<NeedView>>.Handle(GetNeedQuery query,
    CancellationToken cancellationToken)
        {
            var result = await _db.QueryAsync<Entity.NeedView>(
                new CommandDefinition(
                    commandText: Entity.NeedView.Sql,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        State = new[] { NeedState.Active }.Select(_=>_.ToString())
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return result.Select(_ => new NeedView(
                imgsLink: _.ImgLinks?.ParseJson<IEnumerable<String?>>() ?? new String[] { },
                mdBody: _.MdBody,
                state: _.State
            ));
        }
    }
}