using Data;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(identityOptions =>
            {
                //Password settings
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireLowercase = false;

                //UserName Settings
                identityOptions.User.RequireUniqueEmail = true;
                identityOptions.SignIn.RequireConfirmedEmail = false;

                //Lockout Settings
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
