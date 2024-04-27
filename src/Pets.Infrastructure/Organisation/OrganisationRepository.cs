namespace Pets.Infrastructure.Organisation;

using System.Threading;

using Domain.Organisation;
using Domain.ValueTypes;

using Microsoft.EntityFrameworkCore;

public sealed class OrganisationRepository : IOrganisationGetter
{
    private readonly OrganisationDbContext _context;

    public OrganisationRepository(OrganisationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// get Question by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Organisation?> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _context.Organisations.AsNoTracking()
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
}