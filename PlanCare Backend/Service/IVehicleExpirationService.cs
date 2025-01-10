using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service;

public interface IVehicleExpirationService
{
    event Action<HashSet<Vehicle>> OnVehiclesExpired;

    void StartService();
    void StopService();
}