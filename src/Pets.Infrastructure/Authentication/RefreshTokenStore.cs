namespace Pets.Infrastructure.Authentication;

using System.Linq;
using System.Threading;

using Domain.Authentication;
using Domain.Authentication.Exceptions;

using Microsoft.EntityFrameworkCore;

/// <summary>
///     пока что будем хранить в бд
/// </summary>
public sealed class RefreshTokenStore : IRefreshTokenStore
{
    private readonly AuthenticationDbContext _context;
    private readonly TimeSpan _lifeTime = TimeSpan.FromDays(1);

    public RefreshTokenStore(AuthenticationDbContext context)
    {
        _context = context;
    }

    public async Task<String> Issue(Guid userId, String ip, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken(Guid.NewGuid().ToString(), userId, ip, DateTime.UtcNow.Add(_lifeTime));

        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return refreshToken.Id;
    }

    public async Task<(String refreshToken, Guid userId)> Reissue(String refreshToken, String ip, CancellationToken cancellationToken)
    {
        var oldRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(token =>
            token.Id == refreshToken
            && token.ExpireDate > DateTime.UtcNow, cancellationToken);

        if (oldRefreshToken == null)
            throw new UnauthorizedException();

        var dd = DateTime.UtcNow.AddSeconds(-1 * _lifeTime.Seconds * 0.1);
        // берём токен которому ещё жить более 10%
        var newRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(token =>
                                  token.UserId == oldRefreshToken.UserId
                                  && token.ExpireDate > dd, cancellationToken) ??
                              new RefreshToken(Guid.NewGuid().ToString(), oldRefreshToken.UserId, ip, DateTime.UtcNow.Add(_lifeTime));

        if (newRefreshToken.Id != oldRefreshToken.Id)
        {
            oldRefreshToken.Terminate();
            await _context.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return (newRefreshToken.Id, oldRefreshToken.UserId);
    }

    public async Task ExpireAllTokens(Guid userId, CancellationToken cancellationToken)
    {
        var activeRefreshTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .Where(rt => rt.ExpireDate > DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        foreach (var activeRefreshToken in activeRefreshTokens)
            activeRefreshToken.Terminate();

        await _context.SaveChangesAsync(cancellationToken);
    }
}