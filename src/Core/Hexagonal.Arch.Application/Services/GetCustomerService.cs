using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Ports;

namespace Hexagonal.Arch.Application.Services;

public class GetCustomerService(ICustomerRepository customerRepository) : IGetCustomerService
{
    public async Task<Customer> GetAsync(int id)
        => await customerRepository.GetByIdAsync(id);
    
}