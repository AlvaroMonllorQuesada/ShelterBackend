namespace Shelter.API.Features.Animals.GetAnimals;

public class GetAnimalsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string HealthStatus { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string UbicationName { get; set; } = null!;
    public string ShelterZoneName { get; set; } = null!;

    public static explicit operator GetAnimalsResponse(Animal animal) =>
        new()
        {
            Id = animal.Id,
            Name = animal.Name,
            Status = animal.Status,
            UbicationName = animal.UbicationName ?? string.Empty,
        };
}