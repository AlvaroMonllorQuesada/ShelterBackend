using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class Animal
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Species { get; set; } = null!;

    public int? Age { get; set; }

    public string? HealthStatus { get; set; }

    public DateTime? AdmissionDate { get; set; }

    public DateTime? AdoptionDate { get; set; }

    public string? UbicationName { get; set; }

    public string Status { get; set; } = null!;

    public int ShelterId { get; set; }

    public virtual ICollection<AnimalHealthRecord> AnimalHealthRecords { get; set; } = [];

    public virtual ICollection<AnimalMedium> AnimalMedia { get; set; } = [];

    public virtual ICollection<AnimalTreatment> AnimalTreatments { get; set; } = [];

    public virtual Shelter Shelter { get; set; } = null!;
}
