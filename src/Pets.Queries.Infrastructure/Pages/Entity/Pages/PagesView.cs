namespace Pets.Queries.Infrastructure.Pages.Entity.Pages;

using System;

internal sealed class PageView
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
and Id = @PageId
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