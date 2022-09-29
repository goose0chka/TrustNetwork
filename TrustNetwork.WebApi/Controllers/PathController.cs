using Microsoft.AspNetCore.Mvc;
using TrustNetwork.BL.DTO;

namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PathController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> SendToNetwork(MessageDto message)
    {
        throw new NotImplementedException();
    }
}
