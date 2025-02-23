using Shelter.Infrastructure.Media;

namespace Shelter.API.Features.Animals.Images.AddImage;
public record struct Request(
    [FromServices] AnimalShelterDbContext DbContext,
    [FromServices] IMediaService MediaService,
    [FromForm] IFormFileCollection Files,
    [FromRoute] int Id
);
