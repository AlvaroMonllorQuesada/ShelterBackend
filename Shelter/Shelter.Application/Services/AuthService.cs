using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shelter.Infrastructure.Data;
using Shelter.Shared.DTOs;

namespace Shelter.Application.Services
{
    public class AuthService
    {
        private readonly AnimalShelterDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(AnimalShelterDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string?> Authenticate(VolunteerDto volunteerDto)
        {
            var user = _context.Volunteers.FirstOrDefault(u => u.UserName == volunteerDto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(volunteerDto.Password, user.PasswordHash))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Register(VolunteerDto volunteerDto)
        {
            if (_context.Volunteers.Any(u => u.UserName == volunteerDto.UserName))
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(volunteerDto.Password);
            var volunteer = _mapper.Map<Volunteer>(volunteerDto);
            _context.Volunteers.Add(new Volunteer { UserName = volunteer.UserName, PasswordHash = hashedPassword, Role = "User" });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}





