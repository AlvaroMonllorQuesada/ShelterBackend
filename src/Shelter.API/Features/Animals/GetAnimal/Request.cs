

namespace Shelter.API.Features.Animals.GetAnimal;

public record struct Request(
    [FromServices] AnimalShelterDbContext DbContext,
    [FromRoute] int Id
);