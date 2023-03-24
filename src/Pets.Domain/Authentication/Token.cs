namespace Pets.Domain.Authentication;

public record Token(String AccessToken, TimeSpan ExpiresIn, String RefreshToken);