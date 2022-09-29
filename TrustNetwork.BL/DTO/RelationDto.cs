using System.ComponentModel.DataAnnotations;

namespace TrustNetwork.BL.DTO;

public class RelationDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, 10)]
    public int Trust { get; set; }
}
