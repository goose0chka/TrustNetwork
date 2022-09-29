using Microsoft.AspNetCore.Mvc;
namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> SendMessage()
    {
        throw new NotImplementedException();
    }
}
