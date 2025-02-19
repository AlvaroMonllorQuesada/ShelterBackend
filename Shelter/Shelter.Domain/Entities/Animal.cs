using Shelter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.Entities
{
    public class Animal: IAggregateRoot
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Species { get; private set; }
        public int? Age { get; private set; }
        public string HealthStatus { get; private set; }
        public DateTime AdmissionDate { get; private set; }
        public DateTime? AdoptionDate { get; private set; }
        public string Status { get; private set; }
        public int ShelterId { get; private set; }
        public int ShelterZoneId { get; private set; }
        public ShelterZone ShelterZone { get; private set; }
        public List<AnimalImage> Images { get; private set; } = new();

        private Animal() { }

        public Animal(string name, string species, int? age, string healthStatus, DateTime admissionDate, int shelterId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(species)) throw new ArgumentException("Species is required.");
            if (shelterId <= 0) throw new ArgumentException("Valid ShelterId is required.");

            Name = name;
            Species = species;
            Age = age;
            HealthStatus = healthStatus;
            AdmissionDate = admissionDate;
            Status = "Available"; // Default status
            ShelterId = shelterId;
        }

        public void Adopt()
        {
            if (Status == "Adopted") throw new InvalidOperationException("Animal is already adopted.");
            Status = "Adopted";
            AdoptionDate = DateTime.UtcNow;
        }

        public void UpdateHealthStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("Health status cannot be empty.");
            HealthStatus = newStatus;
        }

        public void AssignImage(string imageUrl, bool isPrimary = false)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL is required.");

            if (isPrimary)
                Images.ForEach(img => img.SetAsSecondary()); // Unset previous primary image

            Images.Add(new AnimalImage(imageUrl, isPrimary));
        }
    }
}
