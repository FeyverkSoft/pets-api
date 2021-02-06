using System;
using System.Threading;
using System.Threading.Tasks;

using Pets.Domain.Authentication;

namespace Pets.Infrastructure.Authentication
{
    public sealed class UserRepository : IUserRepository
    {
        public async Task<User> Find(String? username, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}