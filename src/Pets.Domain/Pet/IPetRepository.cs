namespace Pets.Domain.Pet;

using Entity;

/// <summary>
///     Репозиторий питомец
/// </summary>
public interface IPetRepository
{
    /// <summary>
    ///     Получить питомца по его id
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="organisation"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Pet?> GetAsync(Guid petId, Organisation organisation, CancellationToken cancellationToken);

    /// <summary>
    ///     Сохранить питомца
    /// </summary>
    /// <param name="pet"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveAsync(Pet pet, CancellationToken cancellationToken);
}