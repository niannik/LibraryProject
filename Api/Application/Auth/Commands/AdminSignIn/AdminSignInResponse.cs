namespace Application.Auth.Commands.AdminSignIn;

public class AdminSignInResponse
{
    public required string JwtToken { get; set; }
    public required string RefreshToken { get; set; }
}
