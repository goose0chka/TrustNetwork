namespace TrustNetwork.DAL.Model;
public class Relation
{
    public string PersonId { get; set; } = string.Empty;
    public Person Person { get; set; } = null!;
    public string ContactId { get; set; } = string.Empty;
    public Person Contact { get; set; } = null!;
    public int TrustLevel { get; set; }
}
