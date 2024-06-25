using Hexagonal.Arch.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Arch.Application;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddAppicationServices(this IServiceCollection services)
        => services.AddScoped<ICreateCustomerService, CreateCustomerService>();
}
