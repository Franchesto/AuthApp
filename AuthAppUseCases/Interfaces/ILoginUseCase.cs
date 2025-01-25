using AuthAppCore.Models;

namespace AuthAppUseCases.Interfaces
{
    public interface ILoginUseCase
    {
        Task<string> ExecuteAsync(UserLogin userLogin);
    }
}