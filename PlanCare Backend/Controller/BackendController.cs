using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanCare_Backend.Model;
using PlanCare_Backend.Service;

[Route("api/[controller]")]
[ApiController]
public class BackendController : ControllerBase
{
    private readonly IDbService dbService;
    
    public BackendController(IDbService dbService)
    {
        this.dbService = dbService;
    }

    [HttpGet("GetVehicles")]
    public async Task<IActionResult> GetVehicles([FromQuery] string make = "")
    {
        List<Vehicle> result = await dbService.GetVehiclesAsync(make);
        
        return Ok(result);
    }
}