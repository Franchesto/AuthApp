using AuthAppCore.Models;
using AuthAppInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAppInfrastructure.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserDbContext _context;
        private readonly ITokenProviderService tokenProviderService;

        public AuthServices(UserDbContext userDbContext, ITokenProviderService tokenProviderService)
        {
            _context = userDbContext;
            this.tokenProviderService = tokenProviderService;
        }

        public async Task Regiter(User user)
        {
            if (user == null)
            {
                return;
            }

            HashService.HashPassword(user);

            user.CreatedAt = DateTime.Now;

            await _context.Users.AddAsync(user);

            var result = await _context.SaveChangesAsync();

            if (result != 1)
            {
            }
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username.Equals(userLogin.Username));

            if (user == null) 
            {
                throw new UnauthorizedAccessException("Username or password is invalid");
            }
            
            var result = HashService.VerifyPassword(user, userLogin.Password);

            if (result == 0)
            {
                throw new UnauthorizedAccessException("Username or password is invalid");
            }

            var token = tokenProviderService.CreateToken(user);

            return token;
        }
    }
}
