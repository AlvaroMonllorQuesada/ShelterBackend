using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class Volunteer
{
    public int Id { get; set; }

    public string? PhoneNumber { get; set; }

    public string? City { get; set; }

    public DateTime? JoinDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Role { get; set; }

    public int? RoleId { get; set; }

    public string UserName { get; set; } = null!;

    public string NormalizedUserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public virtual UserRole? RoleNavigation { get; set; }

    public virtual ICollection<VolunteerShiftAssignment> VolunteerShiftAssignments { get; set; } = new List<VolunteerShiftAssignment>();

    public virtual ICollection<VolunteerShift> VolunteerShifts { get; set; } = new List<VolunteerShift>();
}
