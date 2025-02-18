namespace FSTime.Contracts.Authorization;

public record RefreshTokenResponse(string UserName, string AccessToken, DateTime AccessTokenExpires);
