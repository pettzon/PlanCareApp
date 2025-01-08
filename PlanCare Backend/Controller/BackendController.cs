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
    public async Task<IActionResult> GetVehicles()
    {
        List<Vehicle> result = await dbService.GetVehiclesAsync();
        
        return Ok(result);
    }
}