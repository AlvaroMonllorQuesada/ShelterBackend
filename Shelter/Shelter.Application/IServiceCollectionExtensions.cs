using Microsoft.Extensions.DependencyInjection;
using Shelter.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Application
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddAutoMapper(typeof(IServiceCollectionExtensions));
            return services;
        }

    }
}
