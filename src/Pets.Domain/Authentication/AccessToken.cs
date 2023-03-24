namespace Pets.Domain.Authentication;

public record AccessToken(String Value, TimeSpan ExpiresIn);