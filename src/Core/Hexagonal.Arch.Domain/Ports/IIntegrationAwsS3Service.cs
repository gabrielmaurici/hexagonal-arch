using Hexagonal.Arch.Domain.Models;

namespace Hexagonal.Arch.Domain.Ports;

public interface IIntegrationAwsS3Service
{
    Task<AddressAwsS3Model?> GetAddressByCepAsync(string cep);
    Task UploadCepAsync(AddressAwsS3Model address);
}