using Shelter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Domain.Repositories
{
    public interface IVolunteerRepository
    {
        Task<Volunteer> GetByIdAsync(int volunteerId);
        Task<IReadOnlyCollection<Volunteer>> GetAllAsync();
        Task AddAsync(Volunteer volunteer);
        Task UpdateAsync(Volunteer volunteer);
        Task DeleteAsync(Volunteer volunteer);
    }
}
