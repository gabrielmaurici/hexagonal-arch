using Hexagonal.Arch.Domain.Entities;

namespace Hexagonal.Arch.Domain.Ports;

public interface IGetCustomerService
{
    Task<Customer> GetAsync(int id);    
}