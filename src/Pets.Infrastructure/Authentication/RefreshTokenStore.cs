using System;
using System.Threading;
using System.Threading.Tasks;

using Pets.Domain.Authentication;

namespace Pets.Infrastructure.Authentication
{
    public sealed class RefreshTokenStore : IRefreshTokenStore
    {
        public async Task<String> Issue(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<(String refreshToken, Guid userId)> Reissue(String refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}