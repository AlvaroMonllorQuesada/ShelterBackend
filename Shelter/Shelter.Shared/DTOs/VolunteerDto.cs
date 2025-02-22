using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Shared.DTOs
{
    public class VolunteerDto
    {
        public int Id { get; set; } = 0;

        public string? PhoneNumber { get; set; } = null!;

        public string? City { get; set; } = null!;

        public DateTime? JoinDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = null!;

        public string? Role { get; set; } = null!;

        public int? RoleId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string NormalizedUserName { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
