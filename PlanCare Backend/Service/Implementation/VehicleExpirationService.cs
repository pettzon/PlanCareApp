﻿using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using PlanCare_Backend.Data;
using PlanCare_Backend.Hub;
using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service.Implementation;

public sealed class VehicleExpirationService : IHostedService, IDisposable
{
    public static event Action<HashSet<Vehicle>>? OnVehiclesExpired;

    private readonly IDbService dbService;
    private readonly IHubContext<RegistrationLiveHub, IExpirationServiceClientMethods> hubContext;

    private readonly CancellationTokenSource checkExpirationTimerCancellationTokenSource = new CancellationTokenSource(); 
    private readonly PeriodicTimer checkExpirationTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));

    public VehicleExpirationService(IDbService dbService, IHubContext<RegistrationLiveHub, IExpirationServiceClientMethods> hubContext)
    {
        this.dbService = dbService;
        this.hubContext = hubContext;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => CheckExpirationAsync(checkExpirationTimerCancellationTokenSource.Token));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await checkExpirationTimerCancellationTokenSource.CancelAsync();
    }

    public void Dispose()
    {
        
    }
    
    private async Task CheckExpirationAsync(CancellationToken cancellationToken)
    {
        while (await checkExpirationTimer.WaitForNextTickAsync(cancellationToken) && !cancellationToken.IsCancellationRequested)
        {
            HashSet<Vehicle> vehicles = await dbService.GetVehiclesAsync();
            HashSet<Vehicle> expiredVehicles = vehicles.Where(v => v.VehicleStatus != VehicleStatus.EXPIRED && v.EvaluateStatus() == VehicleStatus.EXPIRED).ToHashSet();

            if (expiredVehicles.Count == 0)
            {
                continue;
            }
            
            Debug.WriteLine($"Vehicles expired this iteration : {expiredVehicles.Count}");
            OnVehiclesExpired?.Invoke(expiredVehicles);
            await hubContext.Clients.All.UpdateExpirationStatus(expiredVehicles);
        }
    }
}