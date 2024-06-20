namespace Hexagonal.Arch.Domain.Models;

public record AddressAwsS3Model(
    string Cep,
    string Logradouro,
    string Bairro
);