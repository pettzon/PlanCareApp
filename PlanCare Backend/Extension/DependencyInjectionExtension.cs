using PlanCare_Backend.Service;
using PlanCare_Backend.Service.Implementation;

namespace PlanCare_Backend.Extension;

public static class DependencyInjectionExtension
{
    public static void AddExtraServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbService, MockDbService>(); // Binding the interface to the mock DB service instead
        services.AddHostedService<VehicleExpirationService>();
    }
}