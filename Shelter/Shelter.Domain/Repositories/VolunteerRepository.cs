using Shelter.Domain.Entities;
using Shelter.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly AnimalShelterDbContext _animalShelterDbContext;

        public VolunteerRepository(AnimalShelterDbContext animalShelterDbContext)
        {
            
        }

        public Task AddAsync(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Volunteer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Volunteer> GetByIdAsync(int volunteerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }
    }
}
