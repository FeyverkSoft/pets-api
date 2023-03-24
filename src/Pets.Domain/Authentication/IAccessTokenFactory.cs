namespace Pets.Domain.Authentication;

public interface IAccessTokenFactory
{
    Task<AccessToken> Create(User user, CancellationToken cancellationToken);
}