
using Microsoft.EntityFrameworkCore;

namespace Shelter.API.Features.Animals.Images.GetImages;

public record Handler() : GetHandlerAsync<GetImageRequest>("animals/{id}/images")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces<GetImagesResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .WithTags("Images")
            .WithGroupName("Animals")
            .WithDisplayName("Get animal images")
            .WithDescription("Get images of an animal");

    protected override async Task<IResult> HandleAsync(GetImageRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = await request.DbContext.AnimalMedia.Where(x => x.AnimalId == request.Id).ToListAsync(cancellationToken);
        if (response is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(response.Select(x => new GetImageResponse(x.MediaUrl, x.MediaType)));
    }
}



