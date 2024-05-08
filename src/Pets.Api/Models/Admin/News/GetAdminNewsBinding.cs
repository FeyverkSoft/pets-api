namespace Pets.Api.Models.Admin.News;

using System.Collections.Generic;

using Types;

/// <summary>
/// 
/// </summary>
public sealed class GetAdminNewsBinding
{
    public Guid? NewsId { get; set; }
    public Int32 Limit { get; set; } = 8;
    public Int32 Offset { get; set; } = 0;
    public List<NewsState>? NewsStatuses { get; set; } = null;
    public Guid? PetId { get; set; } = null;
    public String? Tag { get; set; } = null;
    public String? Filter { get; set; } = null;
}