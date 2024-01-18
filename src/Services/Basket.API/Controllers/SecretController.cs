using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecretController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult GetSecret()
    {
        return Ok("Secret");
    }
}
