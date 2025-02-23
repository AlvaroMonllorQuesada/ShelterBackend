using Microsoft.EntityFrameworkCore;
using Shelter.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddShelterDatabase(this IServiceCollection services)
    {

        services.AddDbContext<AnimalShelterDbContext>(options =>
        {
            options.UseSqlServer("Server=localhost,1433;Database=Shelter;User=sa;Password=Password123;");
        });
        return services;
    }

    public static IServiceCollection AddShelterInMemoryDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AnimalShelterDbContext>(options =>
        {
            options.UseInMemoryDatabase("Shelter");
        });
        return services;
    }
}
