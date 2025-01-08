using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service;

public interface IVehicleExpirationService
{
    event Action<Vehicle> OnVehicleExpired;

    void StartService();
    void StopService();
}