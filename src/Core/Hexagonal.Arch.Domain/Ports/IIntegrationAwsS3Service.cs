namespace Hexagonal.Arch.Domain.Ports;

public interface IIntegrationAwsS3Service
{
    Task UploadCep(dynamic address);
    Task<string> GetAddressByCep(string cep);
}