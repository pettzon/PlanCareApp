using PlanCare_Backend.Data;
using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service.Implementation;

public class DbService : IDbService
{
    public async Task<Vehicle> GetVehicleAsync(string registration, CountryState state)
    {
        // Implement method to get a single vehicle by state & registration
        throw new NotImplementedException();
    }

    public async Task<List<Vehicle>> GetVehiclesAsync(string make = "")
    {
        throw new NotImplementedException();
    }
}