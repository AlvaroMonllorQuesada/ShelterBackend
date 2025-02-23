using Microsoft.EntityFrameworkCore;

namespace Shelter.API.Features.Animals.GetAnimals;
public record Handler() : GetHandlerAsync<Request>("/animals")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces<IEnumerable<GetAnimalsResponse>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .WithTags("Animals")
            .WithGroupName("Animals")
            .WithDisplayName("Get Animals")
            .WithDescription("Get a list of animals");

    protected override async Task<IResult> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var animals = await request.DbContext.Animals
            .Where(a => request.Name == null || a.Name.Contains(request.Name))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var resources = animals.Select(a => (GetAnimalsResponse)a);
        return Results.Ok(resources);
    }
}
