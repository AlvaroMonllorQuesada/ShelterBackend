using AutoMapper;

namespace Shelter.Application.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Shelter.Infrastructure.Data.Volunteer, Shelter.Shared.DTOs.VolunteerDto>();
        CreateMap<Shelter.Shared.DTOs.VolunteerDto, Shelter.Infrastructure.Data.Volunteer>();
    }
}
