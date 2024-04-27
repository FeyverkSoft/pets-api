namespace Pets.Domain.News;

using System.Collections.Generic;

using Entity;

using ValueTypes;

public interface IPetGetter
{
    /// <summary>
    /// get Pets by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Pet?>> GetAsync(Organisation organisation, IEnumerable<Guid> ids, CancellationToken cancellationToken);
}