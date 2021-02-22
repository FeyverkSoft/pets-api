using System;
using System.Threading;
using System.Threading.Tasks;

using Pets.Domain.Authentication.Exceptions;

using Rabbita.Core;

namespace Pets.Domain.Authentication
{
    public sealed class PasswordChangerService
    {
        private readonly IPasswordHasher _passwordHasher;
        //private readonly IConfirmationCodeProvider _confirmationCodeProvider;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly ICommandBus _bus;

        public PasswordChangerService(
            ICommandBus bus,
            IPasswordHasher passwordHasher,
            //IConfirmationCodeProvider confirmationCodeProvider,
            IRefreshTokenStore refreshTokenStore)
        {
            _bus = bus;
            _passwordHasher = passwordHasher;
            //_confirmationCodeProvider = confirmationCodeProvider;
            _refreshTokenStore = refreshTokenStore;
        }

        /// <exception cref="UnauthorizedException" />
        public async Task ChangePasswordViaOldPassword(User user, String oldPassword, String newPassword, CancellationToken cancellationToken)
        {
            if (!_passwordHasher.TestPassword(user, oldPassword))
                throw new UnauthorizedException();

            user.SetPasswordHash(_passwordHasher.GetHash(user, newPassword));

            await _refreshTokenStore.ExpireAllTokens(user.Id, cancellationToken);
        }

        /// <exception cref="UnauthorizedException" />
        public async Task ChangePasswordViaConfirmationCode(User user, String newPassword, CancellationToken cancellationToken)
        {
            user.SetPasswordHash(_passwordHasher.GetHash(user, newPassword));

            await _refreshTokenStore.ExpireAllTokens(user.Id, cancellationToken);
        }

        public void GenerateAndSendConfirmationCode(User user)
        {
            /*  var code = _confirmationCodeProvider.Generate(user.Email);
               _bus.Send()     */
        }
    }
}