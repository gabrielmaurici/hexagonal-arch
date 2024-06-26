using Hexagonal.Arch.Domain.Dtos;

namespace Hexagonal.Arch.Domain.Ports;

public interface ICreateCustomerService
{
    Task CreateAsync(CreateCustomerDto customer);
}