
namespace Shelter.API.Features.Animals.Images.AddImage;

public record Handler() : PostHandlerAsync<Request>("/animals/{id}/images")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .WithTags("Images")
            .WithGroupName("Animals")
            .WithDisplayName("Add Animal")
            .WithDescription("Add images to an animal")
            .DisableAntiforgery();


    protected override async Task<IResult> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var animal = await request.DbContext.Animals.FindAsync([request.Id], cancellationToken);
        if (animal is null)
        {
            return Results.NotFound();
        }

        var path = $"animals/{animal.Id}";
        foreach (var file in request.Files)
        {
            using var streamReader = new StreamReader(file.OpenReadStream());
            var url = await request.MediaService.UploadImageAsyc(path, streamReader, cancellationToken);
            animal.AnimalMedia.Add(new AnimalMedium
            {
                IsPrimary = false,
                MediaUrl = url,
                UploadedDate = DateTime.UtcNow,
                MediaType = file.ContentType
            });
        }
        await request.DbContext.SaveChangesAsync(cancellationToken);

        return Results.Created();
    }
}
