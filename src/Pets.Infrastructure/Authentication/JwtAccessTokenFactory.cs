namespace Pets.Infrastructure.Authentication;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

using Domain.Authentication;

using Helpers;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Types;

public sealed class JwtAccessTokenFactory : IAccessTokenFactory
{
    private readonly JwtAuthOptions _authOptions;

    public JwtAccessTokenFactory(IOptions<JwtAuthOptions> options)
    {
        _authOptions = options.Value;
        if (_authOptions.SecretKey is null)
            throw new ArgumentNullException(nameof(_authOptions.SecretKey));
    }

    public Task<AccessToken> Create(User user, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, user.Id.ToString()),
            new(CustomClaimTypes.OrganisationId, user.OrganisationId.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(CustomClaimTypes.Login, user.Login),
            new(CustomClaimTypes.Scope, user.Permissions.ToJson())
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(_authOptions.LifeTimeMinutes);

        var jwt = new JwtSecurityToken(
            _authOptions.Issuer,
            _authOptions.Audience,
            notBefore: now,
            claims: claimsIdentity.Claims,
            expires: expires,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Task.FromResult(new AccessToken(
            token,
            TimeSpan.FromMinutes(_authOptions.LifeTimeMinutes)));
    }
}