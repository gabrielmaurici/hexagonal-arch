using Hexagonal.Arch.Infra.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Arch.Infra.Db
{
    public static class InfraDbServicesExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration) 
        {   
            services.AddDbContext<HexagonalContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("hexagonal--db")));

            return services;
        }
    }
}