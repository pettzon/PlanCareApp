using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using PlanCare_Backend.Model;
using PlanCare_Backend.Service;
using PlanCare_Backend.Service.Implementation;

namespace PlanCare_Backend.Hub;

public sealed class RegistrationLiveHub : Hub<IExpirationServiceClientMethods>
{
    public RegistrationLiveHub()
    {
        
    }

    private async void OnVehiclesExpired(HashSet<Vehicle> expiredVehicles)
    {
        await BroadcastVehicleStatusAsync(expiredVehicles);
    }

    private async Task BroadcastVehicleStatusAsync(HashSet<Vehicle> expiredVehicles)
    {
        await Clients.Others.UpdateExpirationStatus(expiredVehicles);
    }
    
    public override Task OnConnectedAsync()
    {
        //VehicleExpirationService.OnVehiclesExpired += OnVehiclesExpired;
        Debug.WriteLine($"Connected:{Context.ConnectionId} : Hub : {GetHashCode()}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        //VehicleExpirationService.OnVehiclesExpired -= OnVehiclesExpired;
        Debug.WriteLine($"Disconnected:{Context.ConnectionId} Exception:{exception}");
        return base.OnDisconnectedAsync(exception);
    }
}

public interface IExpirationServiceClientMethods
{
    Task UpdateExpirationStatus(HashSet<Vehicle> expiredVehicles);
}