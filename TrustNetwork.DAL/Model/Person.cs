namespace TrustNetwork.DAL.Model;
public class Person
{
    public string Id { get; set; } = string.Empty;
    public ICollection<string> Topics { get; set; } = new List<string>();
    public ICollection<Relation> Relations { get; set; } = new List<Relation>();
}
