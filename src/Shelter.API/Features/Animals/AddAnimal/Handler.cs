
namespace Shelter.API.Features.Animals.AddAnimal;

public record Handler() : PostHandlerAsync<Request>("/animals")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces<AddAnimalResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .WithTags("Animals")
            .WithGroupName("Animals")
            .WithDisplayName("Add Animal")
            .WithDescription("Add a new animal to the shelter");


    protected override async Task<IResult> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();


        var animal = new Animal
        {
            Name = request.AddAnimalRequest.Name,
            Species = request.AddAnimalRequest.Species,
            Age = request.AddAnimalRequest.Age,
            HealthStatus = request.AddAnimalRequest.HealthStatus,
            AdmissionDate = request.AddAnimalRequest.AdmissionDate,
            AdoptionDate = request.AddAnimalRequest.AdoptionDate,
            UbicationName = request.AddAnimalRequest.UbicationName,
            Status = request.AddAnimalRequest.Status

        };

        request.DbContext.Animals.Add(animal);
        await request.DbContext.SaveChangesAsync(cancellationToken);

        return Results.Created();
    }
}
