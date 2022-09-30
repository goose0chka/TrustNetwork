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

    public async Task SetRelation(string id, IDictionary<string, int> levels)
    {
        if (levels.Values.Any(x => x < 1 && x > 10))
            throw new ArgumentException("Trust level must be in 1-10 range");

        bool noPerson = _context.Persons.All(x => !string.Equals(x.Id, id));
        bool noContacts = levels.Keys.Any(x => !_context.Persons.Any(y => string.Equals(y.Id, x)));
        if (noPerson || noContacts)
            throw new ArgumentException($"No person with given id found");

        var existingRelations = _context.Relations.Where(x => x.PersonId == id && levels.Keys.Contains(x.ContactId));
        var relationsToAdd = levels
            .Where(x => !existingRelations.Select(x => x.ContactId).Contains(x.Key))
            .Select(x => new Relation()
            {
                PersonId = id,
                ContactId = x.Key,
                TrustLevel = x.Value
            });

        foreach (var item in existingRelations)
            item.TrustLevel = levels[item.PersonId];
        _context.Relations.AddRange(relationsToAdd);
        await _context.SaveChangesAsync();
    }
}
