using Microsoft.AspNetCore.Mvc;
using TrustNetwork.BL.DTO;

namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> SendMessage(MessageDto message)
    {
        throw new NotImplementedException();
    }
}
