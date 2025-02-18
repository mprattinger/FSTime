namespace FSTime.Infrastructure.Common;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string? Secret { get; init; }

    public int? AccessTokeExpiryMinutes { get; init; }
    public int? RefreshTokeExpiryMinutes { get; init; }

    public string? Issuer { get; init; }

    public string? Audience { get; init; }

}