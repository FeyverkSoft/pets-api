namespace Pets.Domain.Organisation;

using ValueTypes;

public interface IOrganisationGetter
{
    /// <summary>
    /// get Question by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Organisation?> GetAsync(Guid id, CancellationToken cancellationToken);
}