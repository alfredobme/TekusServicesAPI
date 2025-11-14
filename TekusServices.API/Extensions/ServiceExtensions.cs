using TekusServices.Application.Interfaces;
using TekusServices.Application.Services;
using TekusServices.Application.Utils;
using TekusServices.Domain.Interfaces;
using TekusServices.Infrastructure.Data.Repositories;

namespace TekusServices.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IProviderRepository, ProviderRepository>();
            repositories.AddScoped<IProviderCustomFieldRepository, ProviderCustomFieldRepository>();
            repositories.AddScoped<IServiceRepository, ServiceRepository>();
            repositories.AddScoped<IUserRepository, UserRepository>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProviderCustomFieldService, ProviderCustomFieldService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddHttpClient<CountryService>();
        }
    }
}
