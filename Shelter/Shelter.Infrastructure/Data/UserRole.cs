using System;
using System.Collections.Generic;

namespace Shelter.Infrastructure.Data;

public partial class UserRole
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
