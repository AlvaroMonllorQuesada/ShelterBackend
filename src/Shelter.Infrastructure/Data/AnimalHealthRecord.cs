using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class AnimalHealthRecord
{
    public int Id { get; set; }

    public int AnimalId { get; set; }

    public DateTime VisitDate { get; set; }

    public string? VeterinarianName { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? Medication { get; set; }

    public string? Notes { get; set; }

    public virtual Animal Animal { get; set; } = null!;
}
