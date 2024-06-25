namespace Hexagonal.Arch.Domain.Dtos;

public record CreateCustomerDto(
    string Name,
    string Cpf,
    short? Age,
    string Cep
);