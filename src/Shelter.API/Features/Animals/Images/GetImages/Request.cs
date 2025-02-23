using Shelter.Infrastructure.Media;

namespace Shelter.API.Features.Animals.Images.GetImages;

public record struct GetImageRequest(
    [FromServices] AnimalShelterDbContext DbContext,
    [FromServices] IMediaService MediaService,
    [FromRoute] int Id
);
