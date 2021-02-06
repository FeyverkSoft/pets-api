using System;
using System.Text.Json.Serialization;

namespace Pets.Api.Models.Admin.Authorization
{
    public sealed class TokenView
    {
        /// <summary>
        /// The access token issued by the authorization server
        /// </summary>
        [JsonPropertyName("access_token")]
        public String? AccessToken { get; }

        /// <summary>
        /// The type of the token issued. Example: Bearer 
        /// </summary>
        [JsonPropertyName("token_type")]
        public String? TokenType { get; }

        /// <summary>
        /// The lifetime in seconds of the access token
        /// </summary>
        [JsonPropertyName("expires_in")]
        public Int64? ExpiresIn { get; }

        /// <summary>
        /// The refresh token, which can be used to obtain new  access tokens using the same authorization grant
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public String? RefreshToken { get; }

        public TokenView(String? accessToken, String? tokenType, Int64? expiresIn, String? refreshToken)
            => (AccessToken, TokenType, ExpiresIn, RefreshToken)
                = (accessToken, tokenType, expiresIn, refreshToken);
    }
}