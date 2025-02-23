namespace Shelter.API.Features.Animals.GetAnimal;

public class GetAnimalResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Species { get; set; } = null!;
    public int? Age { get; set; }
    public string? HealthStatus { get; set; }
    public DateTime? AdmissionDate { get; set; }
    public DateTime? AdoptionDate { get; set; }
    public string? UbicationName { get; set; }
    public string Status { get; set; } = null!;

    public static explicit operator GetAnimalResponse(Animal animal) =>
        new()
        {
            Id = animal.Id,
            Name = animal.Name,
            Species = animal.Species,
            Age = animal.Age,
            HealthStatus = animal.HealthStatus,
            AdmissionDate = animal.AdmissionDate,
            AdoptionDate = animal.AdoptionDate,
            UbicationName = animal.UbicationName,
            Status = animal.Status
        };
}