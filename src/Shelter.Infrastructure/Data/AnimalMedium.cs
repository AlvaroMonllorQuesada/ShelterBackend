using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class AnimalMedium
{
    public int Id { get; set; }

    public int AnimalId { get; set; }

    public string MediaType { get; set; } = null!;

    public string MediaUrl { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime UploadedDate { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Animal Animal { get; set; } = null!;
}
