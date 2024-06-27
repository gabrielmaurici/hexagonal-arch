using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Infra.IntegrationAwsS3.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Arch.Infra.IntegrationAwsS3;

public static class IntegrationAwsS3Extensions
{
    public static IServiceCollection AddIntegrationAwsS3(this IServiceCollection services)
        => services.AddScoped<IIntegrationAwsS3Service, IntegrationAwsS3Service>();
}