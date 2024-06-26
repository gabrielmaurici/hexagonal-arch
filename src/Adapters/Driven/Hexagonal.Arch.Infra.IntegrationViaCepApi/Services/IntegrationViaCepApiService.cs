using System.Net;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.Models;
using Hexagonal.Arch.Domain.Ports;
using Newtonsoft.Json;

namespace Hexagonal.Arch.Infra.IntegrationViaCepApi;

public class IntegrationViaCepApiService(HttpClient client) : IIntegrationViaCepApiService
{
    public async Task<AddressViaCepModel> GetAddressByCepAsync(string cep)
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

        var content = await response.Content.ReadAsStringAsync();
        var addressViaCepModel = JsonConvert.DeserializeObject<AddressViaCepModel>(content)!;

        if (addressViaCepModel.Erro == "true")
            throw new CepNotFoundException();

        return addressViaCepModel;
    }
}