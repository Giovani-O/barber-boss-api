using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using BarberBoss.Domain.Repositories.IncomeRepository;
using BarberBoss.Domain.Repositories.UserRepository;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Token;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Infrastructure.Security.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BarberBoss.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddToken(services, configuration);
        AddRepositories(services);
        AddDbContext(services, configuration);

        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
    }

    private static void AddToken(this IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationInMinutes");
        var signingKey = configuration.GetValue<string>("Settings:jwt:SigningKey");

        services.AddScoped<ITokenGenerator, JwtTokenGenerator>(
            config => new JwtTokenGenerator(expirationTimeInMinutes, signingKey!));
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        
        services.AddScoped<IBarberShopReadOnlyRepository, BarberShopRepository>();
        services.AddScoped<IBarberShopWriteOnlyRepository, BarberShopRepository>();
        services.AddScoped<IBarberShopUpdateOnlyRepository, BarberShopRepository>();
        
        services.AddScoped<IIncomeReadOnlyRepository, IncomeRepository>();
        services.AddScoped<IIncomeWriteOnlyRepository, IncomeRepository>();
        services.AddScoped<IIncomeUpdateOnlyRepository, IncomeRepository>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        var version = new Version(8, 0, 35);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}