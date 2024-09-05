using Api.Authorization;
using Api.Middlewares;
using Api.Services;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Api;

public static class ConfigureServices
{
    public const string DevelopmentCorsPolicy = nameof(DevelopmentCorsPolicy);
    public const string ProductionCorsPolicy = nameof(ProductionCorsPolicy);

    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(DevelopmentCorsPolicy,
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });

            options.AddPolicy(ProductionCorsPolicy,
                policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:3000",
                            "https://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddHttpContextAccessor();

        services.AddTransient<ICurrentUserService, CurrentUserService>();


        services.ConfigureSwagger();

        services.ConfigureAuthentication(configuration);
    }

    private static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }

    private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            BearerTokenSettings bearerTokenSettings = new();
            configuration.GetSection("BearerTokenSettings").Bind(bearerTokenSettings);

            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = bearerTokenSettings.Issuer,
                ValidateIssuer = true,
                ValidAudience = bearerTokenSettings.Audience,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerTokenSettings.SecretKey)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            cfg.Events = new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<TokenValidator>();

                    var isTokenValid = await tokenValidatorService.ValidateSecurityStamp(context.Principal);

                    if (isTokenValid)
                        context.Success();
                    else
                        context.Fail("Failed to validate security stamp.");
                }
            };
        });
        services.AddAuthorizationBuilder()
            .AddPolicy(AppAuthorizationPolicies.Admin, builder => builder.RequireRole(AppRoles.Admin))
            .AddPolicy(AppAuthorizationPolicies.User, builder => builder.RequireRole(AppRoles.User));
    }
}