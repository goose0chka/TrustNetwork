using Microsoft.AspNetCore.Mvc;
using TrustNetwork.BL.DTO;

namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> AddPerson(PersonDto person)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id}/trust_connections")]
    public Task<IActionResult> SetRelation(IDictionary<string, int> levels)
    {
        throw new NotImplementedException();
    }
}
