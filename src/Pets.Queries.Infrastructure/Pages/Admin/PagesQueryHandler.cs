namespace Pets.Queries.Infrastructure.Pages.Admin;

using System;
using System.Linq;

using Queries.Pages.Admin;

public sealed class AdminPagesQueryHandler : IRequestHandler<GetAdminPageQuery, AdminPageView?>,
    IRequestHandler<GetAdminPagesQuery, Page<AdminPageView>>
{
    private readonly IDbConnection _db;

    public AdminPagesQueryHandler(IDbConnection db)
    {
        _db = db;
    }

    async Task<AdminPageView?> IRequestHandler<GetAdminPageQuery, AdminPageView?>.Handle(GetAdminPageQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _db.QuerySingleOrDefaultAsync<Entity.Pages.Admin.AdminPageDto>(
            new CommandDefinition(
                Entity.Pages.Admin.AdminPageDto.Sql,
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
        return new AdminPageView(
            result.Id,
            result.OrganisationId,
            result.ImgLink,
            result.MdBody,
            result.UpdateDate
        );
    }

    async Task<Page<AdminPageView>> IRequestHandler<GetAdminPagesQuery, Page<AdminPageView>>.Handle(GetAdminPagesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _db.QueryMultipleAsync(
            new CommandDefinition(
                Entity.Pages.Admin.AdminPageDto.SearchSql,
                new
                {
                    query.OrganisationId,
                    query.Limit,
                    query.Offset,
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        if (result is null)
            return new Page<AdminPageView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = 0
            };

        return new Page<AdminPageView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await result.ReadSingleAsync<Int64>(),
            Items = (await result.ReadAsync<AdminPageView>()).Select(_ => new AdminPageView(
                Id: _.Id,
                ImgLink: _.ImgLink,
                OrganisationId: _.OrganisationId,
                MdBody: _.MdBody,
                UpdateDate: _.UpdateDate
            ))
        };
    }
}