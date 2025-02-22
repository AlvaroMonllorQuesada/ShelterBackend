using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Shelter.Application.Services;

public class AuthService(AnimalShelterDbContext context, IOptions<JwtOptions> options, IMapper mapper)
{
    private readonly AnimalShelterDbContext _context = context;
    private readonly JwtOptions _jwtOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
    private readonly IMapper _mapper = mapper;

    public Task<string?> Authenticate(VolunteerDto volunteerDto)
    {
        var user = _context.Volunteers.FirstOrDefault(u => u.UserName == volunteerDto.UserName);
        if (user == null || !BCrypt.Net.BCrypt.Verify(volunteerDto.Password, user.PasswordHash))
            return Task.FromResult<string?>(null);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role ?? "none")
        }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult<string?>(tokenHandler.WriteToken(token));
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

