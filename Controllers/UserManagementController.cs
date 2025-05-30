using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserManagementController : ControllerBase
{
    [Authorize(Policy = "ModuleAccess")]
    [HttpGet("dashboard")]
    public IActionResult GetDashboard()
    {
        return Ok("You have access to Dashboard");
    }

    [Authorize(Policy = "ModuleAccess")]
    [HttpGet("reports")]
    public IActionResult GetReports()
    {
        return Ok("You have access to Reports");
    }
}
