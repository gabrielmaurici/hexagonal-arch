namespace Hexagonal.Arch.Domain.Models;

public record AddressAwsS3Model(
    string Cep,
    string City,
    string Street,
    string District
);