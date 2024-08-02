using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.ResponseModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtServices
    {
        private readonly SignInManager<User> signInManager;
        public JwtServices(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<LoginResponse> GenerateToken(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes("MySecretKey123456789SecretMy1233456789");
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey) , SecurityAlgorithms.HmacSha256);

            var claims = await GetClaims(user);

            var discriptor = new SecurityTokenDescriptor
            {
                Issuer = "Library",
                Audience = "Library",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = signingCredentials,

                Subject = new ClaimsIdentity(claims)
            };

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;


            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(discriptor);
            LoginResponse loginResponse = new()
            {
                Token = tokenHandler.WriteToken(securityToken)
            };

            return loginResponse;
        }
        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {

            var result = await signInManager.ClaimsFactory.CreateAsync(user);
            return result.Claims;
        }
    }
}
