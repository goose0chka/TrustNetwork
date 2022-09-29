using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrustNetwork.BL.DTO;

public class MessageDto
{
    [Required]
    public string Text { get; set; } = string.Empty;

    [Required]
    public IEnumerable<string> Topics { get; set; } = new List<string>();

    [Required]
    [JsonPropertyName("from_person_id")]
    public string FromPersonId { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("min_trust_level")]
    [Range(1, 10)]
    public int MinTrustLevel { get; set; }
}
