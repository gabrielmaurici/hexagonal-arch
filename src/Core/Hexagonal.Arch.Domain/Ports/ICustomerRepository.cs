using Hexagonal.Arch.Domain.Entities;

namespace Hexagonal.Arch.Domain.Ports;

public interface ICustomerRepository
{
    Task CreateAsync(Customer customer);
    Task<Customer> GetByIdAsync(int id);
}