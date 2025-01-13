using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using PlanCare_Backend.Model;
using PlanCare_Backend.Service;
using PlanCare_Backend.Service.Implementation;

namespace PlanCare_Backend.Hub;

public sealed class RegistrationLiveHub : Hub<IExpirationServiceClientMethods>
{
    private readonly IDbService dbService;
    
    public RegistrationLiveHub(IDbService dbService)
    {
        this.dbService = dbService;
    }
    
    public async Task FetchVehicleData()
    { 
        //Debug.WriteLine($"Client {Context.ConnectionId} requesting FetchVehicleData()"); 
        HashSet<Vehicle> allVehicles = await dbService.GetVehiclesAsync(); 
        await Clients.Caller.SendAllVehicleData(allVehicles);
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
    Task SendAllVehicleData(HashSet<Vehicle> allVehicles);
    Task UpdateExpirationStatus(HashSet<Vehicle> expiredVehicles);
}