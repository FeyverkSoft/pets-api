namespace Pets.Infrastructure.Authentication;

using System.Security.Cryptography;
using System.Text;

using Core;

using Domain.Authentication;

public sealed class PasswordHasher : IPasswordHasher
{
    public Boolean TestPassword(User user, String? password)
    {
        return GetHash(user.Id, user.Login, password)
            .IgnoreCaseEquals(user.PasswordHash);
    }

    public String GetHash(User user, String password)
    {
        return GetHash(user.Id, user.Login, password);
    }

    private String GetHash(Guid userId, String login, String password)
    {
        using var crypt = SHA512.Create();
        var hash = new StringBuilder();
        var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(login + password + userId));
        foreach (var theByte in crypto) hash.Append(theByte.ToString("x2"));

        return hash.ToString();
    }
}