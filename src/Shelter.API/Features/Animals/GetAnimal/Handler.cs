using Microsoft.EntityFrameworkCore;

namespace Shelter.API.Features.Animals.GetAnimal;

public record Handler() : GetHandlerAsync<Request>("/animals/{id}")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces<GetAnimalResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .WithTags("Animals")
            .WithGroupName("Animals")
            .WithDisplayName("Get Animal")
            .WithDescription("Get an animal by its id");

    protected override async Task<IResult> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var animal = await request.DbContext.Animals.FindAsync([request.Id], cancellationToken);

        if (animal is null)
        {
            return Results.NotFound();
        }
        var resource = (GetAnimalResponse)animal;
        return Results.Ok(resource);
    }
}