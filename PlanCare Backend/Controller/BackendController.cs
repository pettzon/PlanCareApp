using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanCare_Backend.Model;
using PlanCare_Backend.Service;

[Route("api/[controller]")]
[ApiController]
public class BackendController : ControllerBase
{
    private readonly IDbService dbService;
    private readonly IVehicleExpirationService vehicleExpirationService;
    
    public BackendController(IDbService dbService, IVehicleExpirationService vehicleExpirationService)
    {
        this.dbService = dbService;
        this.vehicleExpirationService = vehicleExpirationService;
        
        vehicleExpirationService.StartService();
    }

    [HttpGet("GetVehicles")]
    public async Task<IActionResult> GetVehicles([FromQuery] string make = "")
    {
        HashSet<Vehicle> result = await dbService.GetVehiclesAsync(make);
        
        return Ok(result);
    }
}