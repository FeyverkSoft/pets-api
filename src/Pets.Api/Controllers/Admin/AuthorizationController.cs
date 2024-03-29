﻿using static System.String;

namespace Pets.Api.Controllers.Admin;

using Authorization;

using Domain.Authentication;
using Domain.Authentication.Exceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Admin.Authorization;

using Types;

[ApiController]
[AllowAnonymous]
[ProducesResponseType(typeof(ErrorView), 400)]
public sealed class AuthorizationController : ControllerBase
{
    /// <summary>
    ///     user authentication
    ///     see https://tools.ietf.org/html/rfc6749
    /// </summary>
    /// <see cref="https://tools.ietf.org/html/rfc6749" />
    [HttpPost("oauth2/token")]
    [ProducesResponseType(200, Type = typeof(TokenView))]
    [ProducesResponseType(401, Type = typeof(ErrorView))]
    public async Task<IActionResult> Auth(
        [FromForm] AuthBinding binding,
        [FromServices] AuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        switch (binding.GrantType)
        {
            case GrantType.password:

                if (IsNullOrEmpty(binding.UserName))
                    BadRequest(ErrorView.Build(O2AuthErrorCode.InvalidRequest, $"Field 'username' is required for '{GrantType.password}' grant type"));

                if (IsNullOrEmpty(binding.Password))
                    BadRequest(ErrorView.Build(O2AuthErrorCode.InvalidRequest, $"Field 'password' is required for '{GrantType.password}' grant type"));

                try
                {
                    var (accessToken, expiresIn, refreshToken) =
                        await authenticationService.AuthenticationByPassword(binding.UserName, binding.Password, HttpContext.GetIp(), cancellationToken);
                    return Ok(new TokenView(accessToken, "Bearer", (Int64)expiresIn.TotalSeconds, refreshToken));
                }
                catch (UnauthorizedException)
                {
                    return BadRequest(ErrorView.Build(O2AuthErrorCode.UnauthorizedClient, "Email or password is incorrect"));
                }

            case GrantType.refresh_token:
                if (IsNullOrEmpty(binding.RefreshToken))
                    BadRequest(ErrorView.Build(O2AuthErrorCode.InvalidRequest,
                        $"Field 'refresh_token' is required for '{GrantType.refresh_token}' grant type"));

                try
                {
                    var (accessToken, expiresIn, refreshToken) =
                        await authenticationService.AuthenticationByRefreshToken(binding.RefreshToken, HttpContext.GetIp(), cancellationToken);
                    return Ok(new TokenView(accessToken, "Bearer", (Int64)expiresIn.TotalSeconds, refreshToken));
                }
                catch (UnauthorizedException)
                {
                    return BadRequest(ErrorView.Build(O2AuthErrorCode.UnauthorizedClient, "Refresh token is incorrect"));
                }

            default:
                return BadRequest(ErrorView.Build(O2AuthErrorCode.UnsupportedGrantType, $"Unsupported grant type: {binding.GrantType}."));
        }
    }
}