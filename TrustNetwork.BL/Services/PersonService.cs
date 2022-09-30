using Microsoft.EntityFrameworkCore;
using TrustNetwork.BL.DTO;
using TrustNetwork.DAL;
using TrustNetwork.DAL.Model;

namespace TrustNetwork.BL.Services;

public class PersonService
{
    private DataContext _context;

    public PersonService(DataContext context)
    {
        _context = context;
    }

    public async Task AddPerson(PersonDto dto)
    {
        dto.Id = dto.Id.Trim();
        if (_context.Persons.Any(x => string.Equals(x.Id, dto.Id, StringComparison.InvariantCulture)))
            throw new ArgumentException("Person with given id already exists");

        var person = new Person() { Id = dto.Id, };

        var dtoTopics = dto.Topics.Select(x => x.ToLower().Trim());

        var existingTopics = await _context.Topics
            .Where(x => dtoTopics.Contains(x.Name))
            .ToListAsync();

        var topicsToAdd = dtoTopics
            .Where(x => !existingTopics
                .Select(y => y.Name)
                .Contains(x)
                )
            .Select(x => new Topic() { Name = x });

        var personTopics = existingTopics
            .Union(topicsToAdd)
            .Select(x => new PersonTopic()
            {
                Person = person,
                Topic = x
            });

        _context.Persons.Add(person);
        _context.PersonTopics.AddRange(personTopics);
        await _context.SaveChangesAsync();
    }
}
