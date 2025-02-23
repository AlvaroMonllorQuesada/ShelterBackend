using Shelter.Infrastructure.Media;

namespace Shelter.API.Features.Animals.Images.GetImage;

public record struct GetImageRequest(
    [FromServices] AnimalShelterDbContext DbContext,
    [FromServices] IMediaService MediaService,
    [FromRoute] int Id,
    [FromRoute] int AnimalId
);
