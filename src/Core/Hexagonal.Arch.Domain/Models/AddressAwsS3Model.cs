namespace Hexagonal.Arch.Domain.Models;

public record AddressAwsS3Model(
    string Cep,
    string Street,
    string District
);