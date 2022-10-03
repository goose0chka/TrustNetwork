using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TrustNetwork.BL.DTO;
using TrustNetwork.BL.Exceptions;
using TrustNetwork.DAL;
using TrustNetwork.DAL.Model;

namespace TrustNetwork.BL.Services
{
    public class MessagingService
    {
        private DataContext _context;
        public MessagingService(DataContext context)
        {
            _context = context;
        }

        public async Task<IDictionary<string, IEnumerable<string>>> SendMessage(MessageDto message, IEnumerable<string>? usedIds = null)
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

            IDictionary<string, IEnumerable<string>> res = new Dictionary<string, IEnumerable<string>>();
            res.Add(await GetReceivers(message.FromPersonId, message.Topics, message.MinTrustLevel));

            for (int i = 0; i < res.Count; i++)
            {
                var current = res.ElementAt(i);
                var receivers = res.SelectMany(x => x.Value).ToList();
                for (int j = 0; j < receivers.Count; j++)
                {
                    var key = receivers.ElementAt(j);
                    if (res.ContainsKey(key)) continue;

                    var localRec = await GetReceivers(key, message.Topics, message.MinTrustLevel);
                    var value = localRec.Value.Where(x => !receivers.Contains(x) && !string.Equals(x, message.FromPersonId)).ToList();

                    if (!value.Any()) continue;
                    res.Add(key, value);
                    receivers.AddRange(value);
                }
            }

            return res;
        }

        private async Task<KeyValuePair<string, IEnumerable<string>>> GetReceivers(string id, IEnumerable<string> topics, int minTrust)
        {
            var connections = await _context.Relations
                .Include(x => x.Contact)
                    .ThenInclude(x => x.PersonTopics)
                        .ThenInclude(x => x.Topic)
                .Where(x => string.Equals(x.PersonId, id))
                .Where(x => x.TrustLevel >= minTrust)
                .Select(x => x.Contact)
                .ToListAsync();

            var toSend = connections.Where(x => personHasAllTopics(x, topics));
            return new(id, toSend.Select(x => x.Id).ToList());
        }

        private static readonly Func<Person, string, bool> personHasTopic = (person, topic)
            => person.PersonTopics.Select(x => x.Topic.Name).Contains(topic);

        private static readonly Func<Person, IEnumerable<string>, bool> personHasAllTopics = (person, topics)
            => topics.All(x => personHasTopic(person, x));
    }
}
