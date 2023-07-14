namespace Pets.Domain.Pet;

using Core;

using Entity;

/// <summary>
///     Репозиторий питомец
/// </summary>
public interface IPetRepository
{
    /// <summary>
    ///     Получить питомца
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Pet?> GetAsync(Specification<Pet> specification, CancellationToken cancellationToken);

    /// <summary>
    ///     Сохранить питомца
    /// </summary>
    /// <param name="pet"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveAsync(Pet pet, CancellationToken cancellationToken);
}