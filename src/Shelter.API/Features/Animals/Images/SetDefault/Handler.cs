
using Microsoft.EntityFrameworkCore;

namespace Shelter.API.Features.Animals.Images.SetDefault;

public record Handler() : PatchHandlerAsync<SetDefaultImageRequest>("animals/{Id:int}/images/{ImageId:int}/default")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status304NotModified)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .WithTags("Images")
            .WithGroupName("Animals")
            .WithDisplayName("Set default image")
            .WithDescription("Set an image as the default image of an animal");

    protected override async Task<IResult> HandleAsync(SetDefaultImageRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var image = await request.DbContext.AnimalMedia.SingleOrDefaultAsync(x => x.AnimalId == request.Id && x.Id == request.ImageId, cancellationToken);
        if (image is null)
        {
            return Results.NotFound();
        }

        var current = await request.DbContext.AnimalMedia.SingleOrDefaultAsync(x => x.AnimalId == request.Id && x.IsPrimary, cancellationToken);
        if (current is not null)
        {
            current.IsPrimary = false;
        }
        image.IsPrimary = true;
        await request.DbContext.SaveChangesAsync(cancellationToken);


        return Results.NoContent();
    }
}
