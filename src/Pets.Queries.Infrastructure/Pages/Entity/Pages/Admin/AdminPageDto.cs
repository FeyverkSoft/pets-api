namespace Pets.Queries.Infrastructure.Pages.Entity.Pages.Admin;

using System;

internal sealed class AdminPageDto
{
    internal static readonly String Sql = @"
select 
    p.Id,
    p.OrganisationId, 
    p.ImgLink,
    p.MdBody,
    p.UpdateDate
from `Page` p
where 1 = 1
and OrganisationId = @OrganisationId
";

    internal static readonly String SearchSql = @"
select 
    count(p.Id)
from `Page` p
where 1 = 1
and OrganisationId = @OrganisationId;

select 
    p.Id,
    p.OrganisationId, 
    p.ImgLink,
    p.MdBody,
    p.UpdateDate
from `Page` p
where 1 = 1
and OrganisationId = @OrganisationId
order by
    p.CreateDate desc
limit @Limit offset @Offset
";

    public String Id { get; }

    /// <summary>
    ///     Id организации
    /// </summary>
    public Guid OrganisationId { get; }

    /// <summary>
    ///     Ссылка на фотку до
    /// </summary>
    public String ImgLink { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; }

    public DateTime UpdateDate { get; }
}