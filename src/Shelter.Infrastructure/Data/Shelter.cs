namespace Shelter.Infrastructure.Data;

public partial class Shelter
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Capacity { get; set; }

    public int CurrentOccupation { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual ICollection<ShelterZone> ShelterZones { get; set; } = new List<ShelterZone>();
}
