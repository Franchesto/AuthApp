using AuthAppCore.Models;

namespace AuthAppUseCases.Interfaces
{
    public interface IRegisterUseCase
    {
        Task ExecuteAsync(User user);
    }
}