namespace Pets.Infrastructure.Pet;

using System.Threading;

using Core;

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
    ///     Получить питомца
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Pet?> GetAsync(Specification<Pet> specification, CancellationToken cancellationToken)
    {
        return await _context.Pets
            .SingleOrDefaultAsync(specification, cancellationToken);
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