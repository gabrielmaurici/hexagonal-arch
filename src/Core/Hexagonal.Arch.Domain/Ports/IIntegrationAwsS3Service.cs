using Hexagonal.Arch.Domain.Models;

namespace Hexagonal.Arch.Domain.Ports;

public interface IIntegrationAwsS3Service
{
    Task<AddressAwsS3Model> GetAddressByCep(string cep);
    Task UploadCep(AddressAwsS3Model address);
}