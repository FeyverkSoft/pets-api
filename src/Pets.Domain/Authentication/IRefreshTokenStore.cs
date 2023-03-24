namespace Pets.Domain.Authentication;

public interface IRefreshTokenStore
{
    Task<String> Issue(Guid userId, String ip, CancellationToken cancellationToken);
    Task<(String refreshToken, Guid userId)> Reissue(String refreshToken, String ip, CancellationToken cancellationToken);
    Task ExpireAllTokens(Guid userId, CancellationToken cancellationToken);
}