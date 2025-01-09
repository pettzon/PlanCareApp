using Microsoft.AspNetCore.SignalR;

namespace PlanCare_Backend.Hub;

public class RegistrationLiveHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task SendMessage(string msg)
    {
        await Clients.All.SendAsync("msg", msg);
    }
}