using Shelter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.Entities;

public class Shelter : IAggregateRoot
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public int Capacity { get; private set; } = 0;
    public int CurrentOccupation { get; private set; } = 0;
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;

    private readonly List<ShelterZone> _zones = new();
    public IReadOnlyCollection<ShelterZone> Zones => _zones.AsReadOnly();

    private Shelter() { }

    public Shelter(string name, string address, string status, int capacity = 0)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address is required.");
        if (capacity <= 0) throw new ArgumentException("Capacity must be greater than 0.");

        Name = name;
        Address = address;
        Status = status;
        Capacity = capacity;
        CurrentOccupation = 0;
        CreationDate = DateTime.UtcNow;
        LastUpdate = DateTime.UtcNow;
    }

    // 🔹 Business Logic
    public void UpdateStatus(string newStatus)
    {
        if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("Status cannot be empty.");
        Status = newStatus;
        LastUpdate = DateTime.UtcNow;
    }

    public void AddAnimal()
    {
        if (CurrentOccupation >= Capacity)
        {
            //throw new InvalidOperationException("Shelter is at full capacity.");
            Status = "Full";
        }
        CurrentOccupation++;
        LastUpdate = DateTime.UtcNow;
    }

    public void RemoveAnimal()
    {
        if (CurrentOccupation == 0) throw new InvalidOperationException("Shelter is already empty.");
        CurrentOccupation--;
        LastUpdate = DateTime.UtcNow;
    }

    public void AddZone(string name, string description, int capacity)
    {
        if (_zones.Any(z => z.Name == name)) throw new InvalidOperationException("Zone with this name already exists.");
        _zones.Add(new ShelterZone(Id, name, description, capacity));
        LastUpdate = DateTime.UtcNow;
    }


}
