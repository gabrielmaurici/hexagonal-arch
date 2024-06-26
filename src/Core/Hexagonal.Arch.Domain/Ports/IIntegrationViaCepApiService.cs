using Hexagonal.Arch.Domain.Models;

namespace Hexagonal.Arch.Domain.Ports;

public interface IIntegrationViaCepApiService
{
    Task<AddressViaCepModel> GetAddressByCepAsync(string cep);    
}
