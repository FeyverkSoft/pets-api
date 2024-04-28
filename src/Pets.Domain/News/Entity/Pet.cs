namespace Pets.Domain.News.Entity;

using System.Collections.Generic;

using ValueTypes;

public sealed record Pet
{
    public Guid Id { get; private set; }
    /// <summary>
    ///     Организация которой принадлежит животное
    /// </summary>
    public Organisation Organisation { get; }
    public String? Name { get; private set; }
    public IEnumerable<News> News { get; private set; }
};