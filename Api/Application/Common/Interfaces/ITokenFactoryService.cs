using Domain.Entities.UserAggregate;

namespace Application.Common.Interfaces;

public interface ITokenFactoryService
{
    public string CreateJwt(User user);
    public string CreateRefreshToken();
}
