using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using PlanCare_Backend.Model;
using PlanCare_Backend.Service;
using PlanCare_Backend.Service.Implementation;

namespace PlanCare_Backend.Hub;

public class RegistrationLiveHub : Hub<IExpirationServiceClientMethods>
{
    public RegistrationLiveHub()
    {
        VehicleExpirationService.OnVehiclesExpired += OnVehiclesExpired;
    }

    private void OnVehiclesExpired(HashSet<Vehicle> expiredVehicles)
    {
        BroadcastVehicleStatus(expiredVehicles);
    }

    private async Task BroadcastVehicleStatus(HashSet<Vehicle> expiredVehicles)
    {
        await Clients.All.UpdateExpirationStatus(expiredVehicles);
    }
    
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}

public interface IExpirationServiceClientMethods
{
    Task UpdateExpirationStatus(HashSet<Vehicle> expiredVehicles);
}