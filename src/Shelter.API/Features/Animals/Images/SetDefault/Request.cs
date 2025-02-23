namespace Shelter.API.Features.Animals.Images.SetDefault;

public record SetDefaultImageRequest(
    [FromServices] AnimalShelterDbContext DbContext,
    [FromRoute] int Id,
    [FromRoute] int ImageId
);
