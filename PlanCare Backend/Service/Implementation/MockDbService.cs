using System.Diagnostics;
using System.Text.Json;
using PlanCare_Backend.Data;
using PlanCare_Backend.Model;

namespace PlanCare_Backend.Service.Implementation;

public class MockDbService : IDbService
{
    HashSet<Vehicle> vehicles = new HashSet<Vehicle>();

    // fields to better control randomization of expiration date for mock data
    private int randomHours = 0;
    private int randomMinutes = 5;
    private int randomSeconds = 30;
    private int randomDataEntries = 10;
    
    public MockDbService()
    {
        CreateMockData();
    }
    
    public async Task<Vehicle> GetVehicleAsync(string registration, StateCode state)
    {
        throw new NotImplementedException();
    }

    public async Task<HashSet<Vehicle>> GetVehiclesAsync(string make = "")
    {
        //async implementation not used for mock data
        return string.IsNullOrEmpty(make) ? vehicles : vehicles.Where(v => v.Make == make).ToHashSet();
    }

    private void CreateMockData()
    {
        Random random = new Random();

        string[] brands =
        [
            "Ford",
            "Holden",
            "Volvo",
            "SAAB",
            "BMW"
        ];
        
        for (int i = 0; i < randomDataEntries; i++)
        {
            StateCode state = (StateCode)random.Next(Enum.GetNames<StateCode>().Length);
            TimeSpan addedTimespan = new TimeSpan(random.Next(randomHours), random.Next(randomMinutes), random.Next(randomSeconds));
            DateTime expirationTime = (DateTime.Now).Add(addedTimespan);
            string make = brands[random.Next(brands.Length)];
            
            vehicles.Add(new Vehicle(make,Guid.NewGuid().ToString(), state, DateTime.Now, expirationTime));
        }
    }
}