using Microsoft.EntityFrameworkCore;

namespace Shelter.API.Features.Animals.HealthRecords.GetHealthRecords;

public record Handler() : GetHandlerAsync<Request>("animals/{id}/health-records")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
            .Produces<GetHealthRecordsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .WithTags("Health Records")
            .WithGroupName("Animals")
            .WithDisplayName("Get Health Records")
            .WithDescription("Get health records for an animal by its id");

    protected override async Task<IResult> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var animal = await request.DbContext.Animals.AnyAsync(a => a.Id == request.Id, cancellationToken);

        if (!animal)
        {
            return Results.NotFound();
        }

        var healthRecords = request.DbContext.AnimalHealthRecords.Where(a => a.AnimalId == request.Id);

        if (request.FromDate.HasValue)
        {
            healthRecords = healthRecords.Where(hr => hr.VisitDate >= request.FromDate);
        }
        if (request.ToDate.HasValue)
        {
            healthRecords = healthRecords.Where(hr => hr.VisitDate <= request.ToDate);
        }

        var sortBy = (AnimalHealthRecord hr) => request.SortBy switch
        {
            "Date" => (object)hr.VisitDate,
            "VeterinarianName" => hr.VeterinarianName,
            "Diagnosis" => hr.Diagnosis,
            "Treatment" => hr.Treatment,
            _ => hr.Id
        };
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            healthRecords = healthRecords.OrderBy(hr => sortBy).ThenBy(hr => request.SortOrder == "desc" ? 1 : 0);
        }
        healthRecords = healthRecords
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        var resources = await healthRecords.ToListAsync(cancellationToken);
        return Results.Ok(new GetHealthRecordsResponse(resources.Select(hr => (GetHealthRecordResponse)hr)));
    }
}