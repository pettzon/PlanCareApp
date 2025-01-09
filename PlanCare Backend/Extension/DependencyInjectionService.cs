using PlanCare_Backend.Service;
using PlanCare_Backend.Service.Implementation;

namespace PlanCare_Backend.Extension;

public static class DependencyInjectionService
{
    public static void AddExtraServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbService, MockDbService>(); // Binding the interface to the mock DB service instead
        services.AddSingleton<IVehicleExpirationService, VehicleExpirationService>(); // custom background service implementation instead of IHostedService / BackgroundService for now
    }
}