using Microsoft.AspNetCore.SignalR;
using PlanCare_Backend.Model;
using PlanCare_Backend.Service;

namespace PlanCare_Backend.Hub;

public class RegistrationLiveHub : Microsoft.AspNetCore.SignalR.Hub
{
    private IVehicleExpirationService vehicleExpirationService;
    
    public RegistrationLiveHub(IVehicleExpirationService vehicleExpirationService)
    {
        this.vehicleExpirationService = vehicleExpirationService;
        vehicleExpirationService.OnVehiclesExpired += OnVehiclesExpired;
    }

    private void OnVehiclesExpired(HashSet<Vehicle> expiredVehicles)
    {
        //Broadcast using SignalR to clients of which vehicles are expired
    }
    
    public async Task SendMessage(string msg)
    {
        await Clients.All.SendAsync("msg", msg);
    }
}