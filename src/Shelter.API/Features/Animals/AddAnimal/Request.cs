using System.ComponentModel.DataAnnotations;

namespace Shelter.API.Features.Animals.AddAnimal;
public record struct Request(
    [FromServices] AnimalShelterDbContext DbContext,
    [FromBody] AddAnimalRequest AddAnimalRequest
);

public class AddAnimalRequest
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
    public string Species { get; set; } = null!;
    [Range(0, int.MaxValue)]
    public int? Age { get; set; }
    public string? HealthStatus { get; set; }
    public DateTime? AdmissionDate { get; set; } = DateTime.UtcNow;
    public DateTime? AdoptionDate { get; set; }
    public string? UbicationName { get; set; }
    public string Status { get; set; } = null!;
}
