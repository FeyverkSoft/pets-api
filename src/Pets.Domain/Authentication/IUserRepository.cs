using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pets.Domain.Authentication
{
    public interface IUserRepository
    {
        Task<User> Find(String? username, CancellationToken cancellationToken);
        Task<User> Get(Guid userId, CancellationToken cancellationToken);
    }
}