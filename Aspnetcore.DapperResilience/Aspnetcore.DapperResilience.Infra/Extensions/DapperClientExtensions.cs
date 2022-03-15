using Aspnetcore.DapperResilience.Infra.DataContexts;
using Aspnetcore.DapperResilience.Infra.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace Aspnetcore.DapperResilience.Infra.Extensions
{
    public static class DapperClientExtensions
    {
        public delegate SqlConnection SqlConnectionFactory();

        public static IServiceCollection AddDapperClient(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<SqlConnectionFactory>(() => new SqlConnection(connectionString));
            services.AddScoped(_ => SqlResiliencyPolicy.GetSqlResiliencyPolicy());
            services.AddScoped<ISqlDapperClient, SqlDapperClient>();

            return services;
        }
    }
}
