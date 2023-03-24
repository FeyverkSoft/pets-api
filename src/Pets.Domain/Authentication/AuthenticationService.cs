namespace Pets.Domain.Authentication;

using Exceptions;

public sealed class AuthenticationService
{
    private readonly IAccessTokenFactory _accessTokenFactory;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(
        IUserRepository userRepository,
        IRefreshTokenStore refreshTokenStore,
        IAccessTokenFactory accessTokenFactory,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _refreshTokenStore = refreshTokenStore;
        _accessTokenFactory = accessTokenFactory;
        _passwordHasher = passwordHasher;
    }

    public async Task<Token> AuthenticationByPassword(String? username, String? password, String ip, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Find(username, cancellationToken);

        if (user == null)
            throw new UnauthorizedException();

        if (!_passwordHasher.TestPassword(user, password))
            throw new UnauthorizedException();

        var refreshToken = await _refreshTokenStore.Issue(user.Id, ip, cancellationToken);

        var accessToken = await _accessTokenFactory.Create(user, cancellationToken);

        return new Token(
            accessToken.Value,
            accessToken.ExpiresIn,
            refreshToken
        );
    }

    public async Task<Token> AuthenticationByRefreshToken(String? refreshToken, String ip, CancellationToken cancellationToken)
    {
        if (refreshToken == null)
            throw new UnauthorizedException();

        var (newRefreshToken, userId) = await _refreshTokenStore.Reissue(refreshToken, ip, cancellationToken);

        if (newRefreshToken == null)
            throw new UnauthorizedException();

        var user = await _userRepository.Get(userId, cancellationToken);

        var accessToken = await _accessTokenFactory.Create(user, cancellationToken);

        return new Token(
            accessToken.Value,
            accessToken.ExpiresIn,
            newRefreshToken
        );
    }
}