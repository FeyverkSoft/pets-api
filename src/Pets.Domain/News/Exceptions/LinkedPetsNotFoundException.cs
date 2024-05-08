namespace Pets.Domain.News.Exceptions;

using System.Collections.Generic;

using Types.Exceptions;

/// <summary>
/// Один или несколько связанных петомцев с новостью не найдены в сисиеме
/// </summary>
public class LinkedPetsNotFoundException : NotFoundException
{
    public LinkedPetsNotFoundException(IEnumerable<Guid> pets) : base($"Linked pets with news not found; ids: {String.Join(";", pets)}")
    {
    }
}
