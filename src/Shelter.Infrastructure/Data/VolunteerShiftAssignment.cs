using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class VolunteerShiftAssignment
{
    public int Id { get; set; }

    public int VolunteerShiftId { get; set; }

    public int VolunteerId { get; set; }

    public virtual Volunteer Volunteer { get; set; } = null!;

    public virtual VolunteerShift VolunteerShift { get; set; } = null!;
}
