namespace TrustNetwork.DAL.Model;
public class Person
{
    public string Id { get; set; } = string.Empty;
    public IEnumerable<PersonTopic> PersonTopics { get; set; } = new List<PersonTopic>();
    public IEnumerable<Relation> Relations { get; set; } = new List<Relation>();
}
