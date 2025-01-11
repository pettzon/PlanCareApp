using Microsoft.AspNetCore.SignalR;
using PlanCare_Backend.Model;
using PlanCare_Backend.Service;

namespace PlanCare_Backend.Hub;

public class RegistrationLiveHub : Hub<IExpirationServiceClientMethods>
{
    private readonly IVehicleExpirationService vehicleExpirationService;
    
    public RegistrationLiveHub(IVehicleExpirationService vehicleExpirationService)
    {
        this.vehicleExpirationService = vehicleExpirationService;
        this.vehicleExpirationService.OnVehiclesExpired += OnVehiclesExpired;
    }

    private void OnVehiclesExpired(HashSet<Vehicle> expiredVehicles)
    {
        BroadcastVehicleStatus(expiredVehicles);
    }

    private async Task BroadcastVehicleStatus(HashSet<Vehicle> expiredVehicles)
    {
        await Clients.All.UpdateExpirationStatus(expiredVehicles);
    }
}

public interface IExpirationServiceClientMethods
{
    Task UpdateExpirationStatus(HashSet<Vehicle> expiredVehicles);
}