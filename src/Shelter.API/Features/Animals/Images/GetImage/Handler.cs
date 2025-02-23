
using Microsoft.EntityFrameworkCore;

namespace Shelter.API.Features.Animals.Images.GetImage;

public record Handler() : GetHandlerAsync<GetImageRequest>("animals/{animalId}/images/{id}")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces<GetImageResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .WithTags("Images")
            .WithGroupName("Animals")
            .WithDisplayName("Get animal image")
            .WithDescription("Get an image of an animal");
    protected override async Task<IResult> HandleAsync(GetImageRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = await request.DbContext.AnimalMedia.SingleOrDefaultAsync(x => x.Id == request.Id && x.AnimalId == request.AnimalId, cancellationToken);
        if (response is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(new GetImageResponse(response.MediaUrl, response.MediaType));
    }
}



