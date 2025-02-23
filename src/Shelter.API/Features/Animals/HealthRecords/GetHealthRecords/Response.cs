
namespace Shelter.API.Features.Animals.HealthRecords.GetHealthRecords
{
    public record GetHealthRecordsResponse(IEnumerable<GetHealthRecordResponse> HealthRecords);

    public record GetHealthRecordResponse(
        int Id,
        string Description,
        DateTime Date,
        string VeterinarianName,
        string Diagnosis,
        string Treatment
    )
    {
        public static explicit operator GetHealthRecordResponse(AnimalHealthRecord v)
        {
            return new GetHealthRecordResponse(
                v.Id,
                "",
                v.VisitDate,
                v.VeterinarianName ?? string.Empty,
                v.Diagnosis ?? string.Empty,
                v.Treatment ?? string.Empty
            );
        }
    }

}