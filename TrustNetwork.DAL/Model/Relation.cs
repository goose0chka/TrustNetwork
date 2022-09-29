namespace TrustNetwork.DAL.Model;
public class Relation
{
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public int ContactId { get; set; }
    public Person Contact { get; set; } = null!;
    public int TrustLevel { get; set; }
}
