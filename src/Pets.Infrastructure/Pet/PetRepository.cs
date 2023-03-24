namespace Pets.Infrastructure.Pet;

using System.Threading;

using Domain.Pet;
using Domain.Pet.Entity;

using Microsoft.EntityFrameworkCore;

public sealed class PetRepository : IPetRepository
{
    private readonly PetDbContext _context;

    public PetRepository(PetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Получить питомца по его id
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="organisation"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Pet?> GetAsync(Guid petId, Organisation organisation, CancellationToken cancellationToken)
    {
        return await _context.Pets.SingleOrDefaultAsync(_ => _.Id == petId &&
                                                             _.Organisation == organisation,
            cancellationToken);
    }

    /// <summary>
    ///     Сохранить питомца
    /// </summary>
    /// <param name="pet"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SaveAsync(Pet pet, CancellationToken cancellationToken)
    {
        var entry = _context.Entry(pet);
        if (entry.State == EntityState.Detached)
            await _context.Pets.AddAsync(pet, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}