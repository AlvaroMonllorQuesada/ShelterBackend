namespace Shelter.API.Features.Animals.HealthRecords.GetHealthRecords;
public record struct Request(
    [FromServices] AnimalShelterDbContext DbContext,
    [FromRoute] int Id,
    [FromQuery] string? SortBy,
    [FromQuery] string? SortOrder,
    [FromQuery] DateTime? FromDate,
    [FromQuery] DateTime? ToDate,
    [FromQuery] int Page = 1,
    [FromQuery] int PageSize = 10

);
