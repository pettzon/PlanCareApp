using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service.Implementation;

public sealed class VehicleExpirationService : IVehicleExpirationService
{
    public event Action<Vehicle>? OnVehicleExpired;

    private readonly IDbService dbService;

    private readonly CancellationTokenSource checkExpirationTimerCancellationTokenSource = new CancellationTokenSource(); 
    private readonly PeriodicTimer checkExpirationTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));

    public VehicleExpirationService(IDbService dbService)
    {
        this.dbService = dbService;
    }
    
    public void StartService()
    {
        Task.Run(() => CheckExpirationAsync(checkExpirationTimerCancellationTokenSource.Token));
    }

    public void StopService()
    {
        checkExpirationTimerCancellationTokenSource.Cancel();
    }

    private async Task CheckExpirationAsync(CancellationToken cancellationToken)
    {
        while (await checkExpirationTimer.WaitForNextTickAsync(cancellationToken) && !cancellationToken.IsCancellationRequested)
        {
            
        }
    }
}