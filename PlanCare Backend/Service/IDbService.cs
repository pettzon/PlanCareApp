using PlanCare_Backend.Data;
using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service;

public interface IDbService
{
    Task<Vehicle> GetVehicleAsync(string registration, StateCode state);
    Task<List<Vehicle>> GetVehiclesAsync(string make = "");
}