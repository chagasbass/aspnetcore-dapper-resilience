using Aspnetcore.DapperResilience.Domain.Repositories;
using Aspnetcore.DapperResilience.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Aspnetcore.DapperResilience.Infra.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAdressRepository, AddressRepository>();
            return services;
        }
    }
}
