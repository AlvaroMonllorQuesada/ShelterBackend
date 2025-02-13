using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class Volunteer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }

    public DateTime? JoinDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Role { get; set; }

    public int RoleId { get; set; }

    public virtual UserRole RoleNavigation { get; set; } = null!;

    public virtual ICollection<VolunteerShiftAssignment> VolunteerShiftAssignments { get; set; } = new List<VolunteerShiftAssignment>();

    public virtual ICollection<VolunteerShift> VolunteerShifts { get; set; } = new List<VolunteerShift>();
}
