namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{

    public static IServiceCollection AddShelterAuth(this IServiceCollection services, Action<JwtOptions>? configureJwt = null)
    {
        services.Configure<JwtOptions>(options => configureJwt?.Invoke(options));
        services.AddScoped<AuthService>();
        services.AddAutoMapper(typeof(IServiceCollectionExtensions));
        return services;
    }

}

