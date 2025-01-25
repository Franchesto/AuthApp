using AuthAppCore.Models;
using AuthAppInfrastructure.Interfaces;
using AuthAppUseCases.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAppUseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IAuthServices authService;
        private readonly ILogger<ILoginUseCase> logger;

        public LoginUseCase(IAuthServices authService, ILogger<ILoginUseCase> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        public async Task<string> ExecuteAsync(UserLogin userLogin)
        {
            try
            {
                var token = await authService.Login(userLogin);

                return token;

            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Write("Testing git changes");
                logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.ToString());                
                throw;
            }               
        }
    }
}
