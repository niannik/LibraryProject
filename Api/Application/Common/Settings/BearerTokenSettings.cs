namespace Application.Common.Settings;

public class BearerTokenSettings
{
    public string SecretKey { set; get; } = default!;
    public string Issuer { set; get; } = default!;
    public string Audience { set; get; } = default!;
    public int AccessTokenExpirationMinutes { set; get; }
    public int RefreshTokenExpirationMinutes { set; get; }
}