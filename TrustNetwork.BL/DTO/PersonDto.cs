namespace TrustNetwork.BL.DTO;

public class PersonDto
{
    public string Id { get; set; } = string.Empty;
    public IEnumerable<string> Topics { get; set; } = new List<string>();
}
