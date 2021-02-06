﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Pets.Domain.Authentication.Exceptions;

namespace Pets.Domain.Authentication
{
    public sealed class AuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IAccessTokenFactory _accessTokenFactory;
        private readonly IPasswordHasher _passwordHasher;

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

        public async Task<Token> AuthenticationByPassword(String? username, String? password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(username, cancellationToken);

            if (user == null)
                throw new UnauthorizedException();

            if (!_passwordHasher.TestPassword(user, password))
                throw new UnauthorizedException();

            var refreshToken = await _refreshTokenStore.Issue(user.Id, cancellationToken);

            var accessToken = await _accessTokenFactory.Create(user, cancellationToken);

            return new Token(
                AccessToken: accessToken.Value,
                ExpiresIn: accessToken.ExpiresIn,
                RefreshToken: refreshToken
            );
        }

        public async Task<Token> AuthenticationByRefreshToken(String? refreshToken, CancellationToken cancellationToken)
        {
            if (refreshToken == null)
                throw new UnauthorizedException();

            var (newRefreshToken, userId) = await _refreshTokenStore.Reissue(refreshToken, cancellationToken);

            if (newRefreshToken == null)
                throw new UnauthorizedException();

            var user = await _userRepository.Get(userId, cancellationToken);

            var accessToken = await _accessTokenFactory.Create(user, cancellationToken);

            return new Token(
                AccessToken: accessToken.Value,
                ExpiresIn: accessToken.ExpiresIn,
                RefreshToken: newRefreshToken
            );
        }
    }
}