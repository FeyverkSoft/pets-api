using System;
using System.Threading;
using System.Threading.Tasks;

using Pets.Domain.Pet.Entity;

namespace Pets.Domain.Pet
{
    /// <summary>
    /// Репозиторий питомец
    /// </summary>
    public interface IPetRepository
    {
        /// <summary>
        /// Получить питомца по его id
        /// </summary>
        /// <param name="petId"></param>
        /// <param name="organisation"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Entity.Pet?> GetAsync(Guid petId, Organisation organisation, CancellationToken cancellationToken);

        /// <summary>
        /// Сохранить питомца
        /// </summary>
        /// <param name="pet"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveAsync(Entity.Pet pet, CancellationToken cancellationToken);
    }
}