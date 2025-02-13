using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class ShelterZone
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ShelterId { get; set; }

    public string Type { get; set; } = null!;

    public int Capacity { get; set; }

    public int CurrentOccupation { get; set; }

    public string Status { get; set; } = null!;

    public virtual Shelter Shelter { get; set; } = null!;

    public virtual ICollection<VolunteerShift> VolunteerShifts { get; set; } = new List<VolunteerShift>();
}
