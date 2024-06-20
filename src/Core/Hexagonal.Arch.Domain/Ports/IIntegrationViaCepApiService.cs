using Hexagonal.Arch.Domain.Models;

namespace Hexagonal.Arch.Domain.Ports;

public interface IIntegrationViaCepApiService
{
    Task<AddressAwsS3Model> GetAddressByCep(string cep);    
}
