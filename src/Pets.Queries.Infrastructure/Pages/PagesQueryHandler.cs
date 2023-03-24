namespace Pets.Queries.Infrastructure.Pages;

using Queries.Pages;

public sealed class PagesQueryHandler : IRequestHandler<GetPageQuery, PageView?>
{
    private readonly IDbConnection _db;

    public PagesQueryHandler(IDbConnection db)
    {
        _db = db;
    }

    async Task<PageView?> IRequestHandler<GetPageQuery, PageView?>.Handle(GetPageQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _db.QuerySingleOrDefaultAsync<Entity.Pages.PageView>(
            new CommandDefinition(
                Entity.Pages.PageView.Sql,
                new
                {
                    query.OrganisationId,
                    PageId = query.Page
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        if (result == null)
            return null;
        return new PageView(
            result.Id,
            result.OrganisationId,
            result.ImgLink,
            result.MdBody,
            result.UpdateDate
        );
    }
}