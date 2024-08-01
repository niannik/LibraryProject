using Common;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.Security.Claims;
using System.Text;

namespace WebFramework
{
    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes("MySecretKey123456789SecretMy1233456789");
                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, 
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true,
                    ValidAudience = "Library",

                    ValidateIssuer = true,
                    ValidIssuer = "Library",

                    //TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null)
                        {
                            throw context.Exception;
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                        var userServices = context.HttpContext.RequestServices.GetRequiredService<UserServices>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser == null)
                            context.Fail("Token secuirty stamp is not valid.");
                        var userId = claimsIdentity.GetUserId<int>();
                        var user = await userServices.FindById(userId);
                        await userServices.UpdateLastLoginDateAsync(user);
                    },
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                        {
                            throw context.AuthenticateFailure;
                        }
                        throw new UnauthorizedAccessException("You Are Unauthorized to access this resource");
                    }
                };
            });

        }
    }
}
