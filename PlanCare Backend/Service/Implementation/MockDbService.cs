using System.Diagnostics;
using System.Text.Json;
using PlanCare_Backend.Data;
using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service.Implementation;

public class MockDbService : IDbService
{
    List<Vehicle> vehicles = new List<Vehicle>();
    
    public MockDbService()
    {
        CreateMockData();
    }
    
    public async Task<Vehicle> GetVehicleAsync(string registration, CountryState state)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        return vehicles;
    }

    private void CreateMockData()
    {
        Random random = new Random();
        
        for (int i = 0; i < 10; i++)
        {
            CountryState state = (CountryState)random.Next(Enum.GetNames<CountryState>().Length);
            TimeSpan addedTimespan = new TimeSpan(0, random.Next(3), random.Next(30));
            DateTime expirationTime = (DateTime.Now).Add(addedTimespan);
            
            vehicles.Add(new Vehicle(Guid.NewGuid().ToString(), state, DateTime.Now, expirationTime));
        }
    }
}