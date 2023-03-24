namespace Pets.Api.Models.Admin.Authorization;

using Newtonsoft.Json;

using Types;

public sealed class ErrorView
{
    /// <summary>
    ///     Error code https://tools.ietf.org/html/rfc6749#section-5.2
    /// </summary>
    [JsonProperty("error")]
    public String Error { get; set; }

    /// <summary>
    ///     Human-readable text providing additional information
    /// </summary>
    [JsonProperty("error_description")]
    public String ErrorDescription { get; set; }

    public static ErrorView Build(O2AuthErrorCode code, String description = null)
    {
        return new ErrorView
        {
            Error = MapErrorCode(code),
            ErrorDescription = description
        };
    }

    private static String MapErrorCode(O2AuthErrorCode code)
    {
        return code switch
        {
            O2AuthErrorCode.InvalidRequest => "invalid_request",
            O2AuthErrorCode.InvalidClient => "invalid_client",
            O2AuthErrorCode.InvalidGrant => "invalid_grant",
            O2AuthErrorCode.UnauthorizedClient => "unauthorized_client",
            O2AuthErrorCode.UnsupportedGrantType => "unsupported_grant_type",
            O2AuthErrorCode.InvalidScope => "invalid_scope",
            _ => throw new ArgumentException($"Unknown code {code}", nameof(code))
        };
    }
}