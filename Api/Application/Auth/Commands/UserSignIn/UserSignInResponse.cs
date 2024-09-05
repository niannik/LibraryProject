namespace Application.Auth.Commands.UserSignIn;

public class UserSignInResponse
{
    public required string JwtToken { get; set; }
    public required string RefreshToken { get; set; }
}
