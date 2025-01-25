using AuthAppCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthAppInfrastructure.Services
{
    public class HashService
    {        

        public static void HashPassword(User user)
        {
            var hash = new PasswordHasher<User>();

            user.PasswordHash = hash.HashPassword(user, user.PasswordHash);
        }

        public static int VerifyPassword(User user, string password)
        {
            var hash = new PasswordHasher<User>();

            var result = hash.VerifyHashedPassword(user, user.PasswordHash, password);

            return ((int)result);
        }
    }
}
