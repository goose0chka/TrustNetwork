namespace TrustNetwork.DAL.Model;
public class Topic
{
    public int TopicId { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<PersonTopic> PersonTopics { get; set; } = new List<PersonTopic>();
}
