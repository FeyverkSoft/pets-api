using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Pets.Queries.Pages;

using Query.Core;

namespace Pets.Queries.Infrastructure.Pages
{
    public sealed class PagesQueryHandler : IQueryHandler<GetPageQuery, PageView?>
    {
        private readonly IDbConnection _db;

        public PagesQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<PageView?> IQueryHandler<GetPageQuery, PageView?>.Handle(GetPageQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _db.QuerySingleAsync<Entity.Pages.PageView>(
                new CommandDefinition(
                    commandText: Entity.Pages.PageView.Sql,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        PageId = query.Page
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return new PageView(
                id: result.Id,
                organisationId: result.OrganisationId,
                imgLink: result.ImgLink,
                mdBody: result.MdBody,
                updateDate: result.UpdateDate
            );
        }
    }
}