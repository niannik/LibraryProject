using Application.Common.Interfaces;
using Application.Common.Settings;
using Domain.Entities.UserAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class DatabaseInitializer
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IConfiguration _configuration;
    private readonly IApplicationDbContext _dbContext;
    private readonly AdminInfo _adminInfo;

    public DatabaseInitializer(IApplicationDbContext dbContext, ILogger<DatabaseInitializer> logger, IConfiguration configuration , AdminInfo adminInfo)
    {
        _logger = logger;
        _configuration = configuration;
        _dbContext = dbContext;
        _adminInfo = adminInfo;
    }

    public virtual async Task CreateDatabaseAsync()
    {
        try
        {
            var admin = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == _adminInfo.Email && x.Password == _adminInfo.Password);
            if(admin is null)
            {
                var newAdmin = new User
                {
                    FirstName = _adminInfo.FirstName,
                    LastName = _adminInfo.LastName,
                    Email = _adminInfo.Email,
                    Password = _adminInfo.Password,
                    PhoneNumber = _adminInfo.PhoneNumber,
                    Address = _adminInfo.Address,
                    LastLoginDate = DateTime.Now,
                    RoleType = RoleTypes.Admin,
                    CreatedAt = DateTime.Now,
                };

                _dbContext.Users.Add(newAdmin);
            }
            await _dbContext.SaveChangesAsync();
            await _dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to create database and apply migrations. details: {exceptionMessage}", ex.Message);
            throw;
        }
    }
}