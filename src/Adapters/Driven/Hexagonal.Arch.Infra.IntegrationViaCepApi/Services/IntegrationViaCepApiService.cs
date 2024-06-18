using System.Net;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.Ports;

namespace Hexagonal.Arch.Infra.IntegrationViaCepApi;

public class IntegrationViaCepApiService(HttpClient client) : IIntegrationViaCepApi
{
    public async Task<string> GetAddressByCep(string cep)
    {
        var resource = $"ws/{cep}/json/";
        var request = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + resource);
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode) 
        {
            throw response.StatusCode switch 
            {
                HttpStatusCode.BadRequest => new InvalidCepFormatException(),
                _ => new Exception("Erro ao consultar CEP do cliente")
            };
        }

        return await response.Content.ReadAsStringAsync();
    }
}