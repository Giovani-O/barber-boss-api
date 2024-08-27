using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BarberShopMappingProfile));
        services.AddAutoMapper(typeof(IncomeMappingProfile));
        services.AddAutoMapper(typeof(UserMappingProfile));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUseCase, RegisterUseCase>();
    }
}