using System;

namespace Pets.Domain.Authentication
{
    public interface IPasswordHasher
    {
        Boolean TestPassword(User user, String? password);

        String GetHash(User user, String password);
    }
}