using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class VolunteerShift
{
    public int Id { get; set; }

    public int ShelterZoneId { get; set; }

    public DateOnly ShiftDate { get; set; }

    public string ShiftType { get; set; } = null!;

    public string ZoneStatus { get; set; } = null!;

    public string? Observations { get; set; }

    public int CreatedBy { get; set; }

    public virtual Volunteer CreatedByNavigation { get; set; } = null!;

    public virtual ShelterZone ShelterZone { get; set; } = null!;

    public virtual ICollection<VolunteerShiftAssignment> VolunteerShiftAssignments { get; set; } = new List<VolunteerShiftAssignment>();
}
