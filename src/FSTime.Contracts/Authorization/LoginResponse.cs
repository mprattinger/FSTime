namespace FSTime.Contracts.Authorization;

public record LoginResponse(string UserName, string AccessToken, DateTime AccessTokenExpires, string RefereshToken, DateTime RefreshTokenExpires);
