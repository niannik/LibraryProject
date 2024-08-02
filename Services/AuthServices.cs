using Common;
using Data;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
using Services.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthServices
    {
        private readonly ApplicationDbContext context;
        private readonly JwtServices jwtServices;
        public AuthServices(ApplicationDbContext context, JwtServices jwtServices)
        {
            this.context = context;
            this.jwtServices = jwtServices;
        }
        public async Task<User?> FindByUserNameAndPass(string userName, string password)
        {
            var hashedPassword = PasswordHasher.GetSha256Hash(password);
            var user = await context.Users.Where(x => x.UserName == userName && x.PasswordHash == hashedPassword)
                .FirstOrDefaultAsync();
            return user;
        }
        public async Task<Result<LoginResponse>> LogInWithPassword(string userName, string password)
        {
            var user = await FindByUserNameAndPass(userName, password);
            if (user == null)
            {
                return UserErrors.UserNotFound;
            }

            return await jwtServices.GenerateToken(user);
        }

    }
}
