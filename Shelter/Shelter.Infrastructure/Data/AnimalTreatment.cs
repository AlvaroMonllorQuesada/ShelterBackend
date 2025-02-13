using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class AnimalTreatment
{
    public int Id { get; set; }

    public int AnimalId { get; set; }

    public string TreatmentType { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual Animal Animal { get; set; } = null!;
}
