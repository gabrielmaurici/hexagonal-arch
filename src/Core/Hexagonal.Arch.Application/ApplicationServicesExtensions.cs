using Hexagonal.Arch.Application.Services;
using Hexagonal.Arch.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Arch.Application;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<ICreateCustomerService, CreateCustomerService>()
                   .AddScoped<IGetCustomerService, GetCustomerService>();
}
