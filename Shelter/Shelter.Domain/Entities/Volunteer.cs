using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.Entities;

public class Volunteer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string City { get; private set; }
    public DateTime JoinDate { get; private set; }
    public string Status { get; private set; }

    private readonly List<VolunteerShift> _shifts = new();
    public IReadOnlyCollection<VolunteerShift> Shifts => _shifts.AsReadOnly();

    private Volunteer() { } // Required by EF Core

    public Volunteer(string name, string lastName, string phoneNumber, string email, string city)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        //if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.");

        Name = name;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        City = city;
        JoinDate = DateTime.UtcNow;
        Status = "Active"; // Default status
    }

    // 🔹 Update Contact Information
    public void UpdateContactInfo(string phoneNumber, string email, string city)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        City = city;
    }

    public void UpdateStatus(string newStatus)
    {
        if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("Status cannot be empty.");
        Status = newStatus;
    }


    public void AssignShift(VolunteerShift shift)
    {
        if (shift == null) throw new ArgumentNullException(nameof(shift));
        if (_shifts.Any(s => s.Date == shift.Date && s.ShiftType == shift.ShiftType))
            throw new InvalidOperationException("Volunteer is already assigned to this shift.");

        _shifts.Add(shift);
    }
}
