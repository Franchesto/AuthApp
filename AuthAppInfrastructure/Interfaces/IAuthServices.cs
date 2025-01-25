using AuthAppCore.Models;

namespace AuthAppInfrastructure.Interfaces
{
    public interface IAuthServices
    {
        Task<string> Login(UserLogin userLogin);
        Task Regiter(User user);
    }
}