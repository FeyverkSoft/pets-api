namespace Pets.Queries.Infrastructure.Organisation.Entity;

using System;

using Types;

internal sealed class ContactView
{
    internal static readonly String Sql = @"
select 
    p.OrganisationId, 
    p.ImgLink,
    p.MdBody,
    p.Type
from `OrganisationContact` p
where 1 = 1
and OrganisationId = @OrganisationId
";

    /// <summary>
    ///     Id организации
    /// </summary>
    public Guid OrganisationId { get; }

    /// <summary>
    ///     Ссылка на значёк
    /// </summary>
    public String? ImgLink { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; }

    public ContactType Type { get; }
}