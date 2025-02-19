using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.Entities;

public class VolunteerShift
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public string ShiftType { get; private set; } // Morning / Afternoon
    public string ShelterZoneStatus { get; private set; } // Clean / Needs Cleaning
    public string Observations { get; private set; }

    private readonly List<Volunteer> _volunteers = new();
    public IReadOnlyCollection<Volunteer> Volunteers => _volunteers.AsReadOnly();

    private VolunteerShift() { }

    public VolunteerShift(DateTime date, string shiftType, string shelterZoneStatus, string observations)
    {
        if (string.IsNullOrWhiteSpace(shiftType)) throw new ArgumentException("Shift type is required.");
        if (string.IsNullOrWhiteSpace(shelterZoneStatus)) throw new ArgumentException("Shelter zone status is required.");

        Date = date;
        ShiftType = shiftType;
        ShelterZoneStatus = shelterZoneStatus;
        Observations = observations;
    }

    public void AddVolunteer(Volunteer volunteer)
    {
        if (volunteer == null) throw new ArgumentNullException(nameof(volunteer));
        if (_volunteers.Any(v => v.Id == volunteer.Id))
            throw new InvalidOperationException("Volunteer is already in this shift.");

        _volunteers.Add(volunteer);
    }

    public void UpdateObservations(string observations)
    {
        Observations = observations;
    }

    public void UpdateShelterZoneStatus(string newStatus)
    {
        if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("Status cannot be empty.");
        ShelterZoneStatus = newStatus;
    }

    public void RemoveVolunteer(Volunteer volunteer)
    {
        if (volunteer == null) throw new ArgumentNullException(nameof(volunteer));
        if (!_volunteers.Any(v => v.Id == volunteer.Id))
            throw new InvalidOperationException("Volunteer is not in this shift.");
        _volunteers.Remove(volunteer);
    }

    public void ClearVolunteers()
    {
        _volunteers.Clear();
    }
}
