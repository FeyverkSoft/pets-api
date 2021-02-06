using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pets.Domain.Authentication
{
    public interface IRefreshTokenStore
    {
        Task<String> Issue(Guid userId, CancellationToken cancellationToken);
        Task<(String refreshToken, Guid userId)> Reissue(String refreshToken, CancellationToken cancellationToken);
    }
}