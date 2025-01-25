using AuthAppCore.Models;

namespace AuthAppInfrastructure.Interfaces
{
    public interface ITokenProviderService
    {
        string CreateToken(User user);
    }
}