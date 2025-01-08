using PlanCare_Backend.Service;
using PlanCare_Backend.Service.Implementation;

namespace PlanCare_Backend.Extension;

public static class DependencyInjectionService
{
    public static void AddExtraServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbService, MockDbService>(); // We use the fake DB service to test things
        services.AddSingleton<IVehicleExpirationService, VehicleExpirationService>(); // custom background service implementation instead of IHostedService / BackgroundService for now
    }
}