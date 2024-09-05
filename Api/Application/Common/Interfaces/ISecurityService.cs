namespace Application.Common.Interfaces;

public interface ISecurityService
{
    string GetSha256Hash(string input);
    string CreateRandomString();
}
