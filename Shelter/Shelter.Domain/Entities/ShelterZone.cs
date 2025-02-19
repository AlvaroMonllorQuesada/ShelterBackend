using Shelter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.Entities;

public class ShelterZone
{
    public int Id { get; private set; }
    public int ShelterId { get; private set; }
    public Shelter Shelter { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Status { get; private set; }
    public int Capacity { get; private set; }
    public int CurrentOccupation { get; private set; }
    public DateTime LastUpdate { get; private set; }

    private readonly List<Animal> _animals = new();
    public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();

    private ShelterZone() { }

    public ShelterZone(int shelterId, string name, string description, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Zone name is required.");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");

        ShelterId = shelterId;
        Name = name;
        Description = description;
        Status = "Clean"; // Default status
        LastUpdate = DateTime.UtcNow;
        Capacity = capacity;
        CurrentOccupation = 0;
    }

    public void UpdateStatus(string newStatus)
    {
        if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("Status cannot be empty.");
        Status = newStatus;
        LastUpdate = DateTime.UtcNow;
    }

    public void AddAnimal(Animal animal)
    {
        if (animal == null) throw new ArgumentNullException(nameof(animal));
        //if (CurrentOccupation >= Capacity) throw new InvalidOperationException("Zone is at full capacity.");
        if (_animals.Any(a => a.Id == animal.Id)) throw new InvalidOperationException("Animal is already in this zone.");

        _animals.Add(animal);
        Shelter.AddAnimal();
        CurrentOccupation++;
        LastUpdate = DateTime.UtcNow;
    }

    public void RemoveAnimal(Animal animal)
    {
        if (animal == null) throw new ArgumentNullException(nameof(animal));
        if (!_animals.Any(a => a.Id == animal.Id)) throw new InvalidOperationException("Animal is not in this zone.");
        _animals.Remove(animal);
        Shelter.RemoveAnimal();
        CurrentOccupation--;
        LastUpdate = DateTime.UtcNow;
    }
}
