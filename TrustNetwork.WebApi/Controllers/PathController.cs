using Microsoft.AspNetCore.Mvc;
namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PathController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> SendToNetwork()
    {
        throw new NotImplementedException();
    }
}
