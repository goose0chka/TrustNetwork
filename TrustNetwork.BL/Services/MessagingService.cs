using Microsoft.EntityFrameworkCore;
using TrustNetwork.BL.DTO;
using TrustNetwork.BL.Exceptions;
using TrustNetwork.DAL;
using TrustNetwork.DAL.Model;

using SendMessageResult = System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>;

namespace TrustNetwork.BL.Services
{
    public class MessagingService
    {
        private DataContext _context;
        public MessagingService(DataContext context)
        {
            _context = context;
        }

        public async Task<SendMessageResult> SendMessage(MessageDto message)
        {
            if (!_context.Persons.Any(x => string.Equals(x.Id, message.FromPersonId)))
                throw new BadRequestException("No person with given id found", "from_person_id");
            if (message.MinTrustLevel < 1 || message.MinTrustLevel > 10)
                throw new BadRequestException("Trust level must be in 1-10 range");
            if (string.IsNullOrWhiteSpace(message.Text))
                throw new BadRequestException("Message text cannot be empty");

            var connections = await _context.Relations
                .Include(x => x.Contact)
                    .ThenInclude(x => x.PersonTopics)
                        .ThenInclude(x => x.Topic)
                .Where(x => string.Equals(x.PersonId, message.FromPersonId))
                .Where(x => x.TrustLevel >= message.MinTrustLevel)
                .Select(x => x.Contact)
                .ToListAsync();

            var toSend = connections.Where(x => personHasAllTopics(x, message.Topics));
            return new SendMessageResult() { { message.FromPersonId, toSend.Select(x => x.Id) } };
        }

        private static readonly Func<Person, string, bool> personHasTopic = (person, topic)
            => person.PersonTopics.Select(x => x.Topic.Name).Contains(topic);

        private static readonly Func<Person, IEnumerable<string>, bool> personHasAllTopics = (person, topics)
            => topics.All(x => personHasTopic(person, x));
    }
}
