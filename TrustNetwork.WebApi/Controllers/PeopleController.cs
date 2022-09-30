using Microsoft.AspNetCore.Mvc;
using TrustNetwork.BL.DTO;
using TrustNetwork.BL.Services;

namespace TrustNetwork.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly PersonService _service;

    public PeopleController(PersonService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddPerson(PersonDto person)
    {
        var res = await _service.AddPerson(person);
        return Created(string.Empty, res);
    }

    [HttpPost("{id}/trust_connections")]
    [ProducesResponseType(201)]
    [ProducesResponseType(402)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> SetRelation(string id, IDictionary<string, int> levels)
    {
        await _service.SetRelation(id, levels);
        return Created(string.Empty, null);
    }
}
