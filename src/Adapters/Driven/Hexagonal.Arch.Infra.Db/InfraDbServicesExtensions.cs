using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Infra.Db.Context;
using Hexagonal.Arch.Infra.Db.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Arch.Infra.Db
{
    public static class InfraDbServicesExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services) 
        {   
            services.AddDbContext<HexagonalContext>(options =>
                options.UseInMemoryDatabase("hexagonal-db")
            );

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}