namespace FSTime.Application.Common.Models;

public record TokenGeneratorResult(string AccessToken, DateTime AccessTokenExpiryDate, string RefreshToken, DateTime RefreshTokenExpiryDate);
