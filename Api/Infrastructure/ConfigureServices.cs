using Application.Common.Interfaces;
using Application.Common.Settings;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.AddSingleton<AppSaveChangeInterceptor>();

        var connectionString = configuration.GetConnectionString("Postgres");

        ArgumentNullException.ThrowIfNull(nameof(connectionString));

        services.AddSingleton(_ =>
        {
            return new NpgsqlDataSourceBuilder(connectionString)
                .EnableDynamicJson()
                .Build();
        });

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(serviceProvider.GetRequiredService<NpgsqlDataSource>());

            options.AddInterceptors(serviceProvider.GetRequiredService<AppSaveChangeInterceptor>());
        });

        services.AddScoped<DatabaseInitializer>();

        services.AddSingleton(_ =>
        {
            BearerTokenSettings bearerTokenSettings = new();
            configuration.GetRequiredSection("BearerTokenSettings").Bind(bearerTokenSettings);
            return bearerTokenSettings;
        });
        services.AddSingleton(_ =>
        {
            AdminInfo adminInfo = new();
            configuration.GetRequiredSection("AdminInfo").Bind(adminInfo);
            return adminInfo;
        });

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<ITokenFactoryService, TokenFactoryService>();
        services.AddScoped<TokenValidator>();

    }
}
