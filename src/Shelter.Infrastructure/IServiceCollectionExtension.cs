using Microsoft.EntityFrameworkCore;
using Shelter.Infrastructure.Data;
using Shelter.Infrastructure.Media;

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
            options.UseInMemoryDatabase("Shelter").UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                context.Set<Shelter.Infrastructure.Data.Shelter>().Add(new Shelter.Infrastructure.Data.Shelter
                {
                    Name = "Shelter",
                    Address = "Address",
                    Capacity = 100,
                    CurrentOccupation = 0,
                    Status = "Status",
                    Animals = [
                        new Animal
                        {
                            Name = "Animal",
                            Species = "Species",
                            Age = 1,
                            HealthStatus = "HealthStatus",
                            AdmissionDate = DateTime.UtcNow,
                            AdoptionDate = DateTime.UtcNow,
                            UbicationName = "UbicationName",
                            Status = "Status"
                        }
                    ]
                });
                await context.SaveChangesAsync(cancellationToken);
            });

        });
        return services;

    }

    public static IServiceCollection AddShelterMediaService(this IServiceCollection services)
    {
        services.AddScoped<IMediaService, MediaService>();
        return services;
    }
}
