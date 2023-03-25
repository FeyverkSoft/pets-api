namespace Pets.Queries;

using System.Collections.Generic;

public sealed class Page<T> where T : class
{
    public Int64 Total { get; set; }

    public Int32 Offset { get; set; }

    public Int32 Limit { get; set; }

    public IEnumerable<T> Items { get; set; }
}