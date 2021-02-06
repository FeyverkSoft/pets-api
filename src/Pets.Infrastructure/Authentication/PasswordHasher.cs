using System;

using Pets.Domain.Authentication;

namespace Pets.Infrastructure.Authentication
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        public Boolean TestPassword(User user, String? password)
        {
            throw new NotImplementedException();
        }

        public String GetHash(User user, String password)
        {
            throw new NotImplementedException();
        }
    }
}