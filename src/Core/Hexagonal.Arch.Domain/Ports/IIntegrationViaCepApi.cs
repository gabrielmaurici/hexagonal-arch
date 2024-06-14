namespace Hexagonal.Arch.Domain.Ports;

public interface IIntegrationViaCepApi
{
    Task<string> GetAddressByCep(string cep);    
}
