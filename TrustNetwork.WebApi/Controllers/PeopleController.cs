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
        await _service.AddPerson(person);
        return Created(string.Empty, null);
    }

    [HttpPost("{id}/trust_connections")]
    public async Task<IActionResult> SetRelation(string id, IDictionary<string, int> levels)
    {
        await _service.SetRelation(id, levels);
        return Created(string.Empty, null);
    }
}
