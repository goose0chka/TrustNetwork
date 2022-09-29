using Microsoft.AspNetCore.Mvc;
namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> AddPerson()
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id}/trust_connections")]
    public Task<IActionResult> SetRelation()
    {
        throw new NotImplementedException();
    }
}
