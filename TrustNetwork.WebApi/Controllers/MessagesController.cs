using Microsoft.AspNetCore.Mvc;
using TrustNetwork.BL.DTO;
using TrustNetwork.BL.Services;

namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly MessagingService _service;

    public MessagesController(MessagingService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SendMessage(MessageDto message)
    {
        var res = await _service.SendMessage(message);
        return Created(string.Empty, res);
    }
}
