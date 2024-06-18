using Hexagonal.Arch.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Arch.Infra.IntegrationViaCepApi;

public static class IntegrationViaCepApiExtensions
{
    public static IServiceCollection AddIntegrationViaCepApi(this IServiceCollection services, IConfiguration configuration)
    { 
        var urlViaCepApi = configuration.GetValue<string>("via-cep-api") ??
            throw new NullReferenceException("Url ViaCepApi não encontrada");

        services.AddHttpClient<IIntegrationViaCepApi, IntegrationViaCepApiService>(client => {
            client.BaseAddress = new Uri(urlViaCepApi);
        });

        return services;
    }
}