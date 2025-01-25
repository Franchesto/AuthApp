using AuthAppCore.Models;
using AuthAppInfrastructure.Interfaces;
using AuthAppUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAppUseCases
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IAuthServices authServices;

        public RegisterUseCase(IAuthServices authServices)
        {
            this.authServices = authServices;
        }

        public async Task ExecuteAsync(User user)
        {
            await authServices.Regiter(user);
        }
    }
}
