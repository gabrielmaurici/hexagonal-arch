using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Domain.ValueObjects;
using Hexagonal.Arch.Infra.Db.Context;
using Hexagonal.Arch.Infra.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Hexagonal.Arch.Infra.Db.Repositories;

public class CustomerRepository(HexagonalContext context) : ICustomerRepository
{
    public async Task CreateAsync(Customer customer)
    {
        var customerEfModel = new CustomerEfModel(
            id: 0,
            customer.Name,
            customer.Age,
            customer.Cpf.Document,
            customer.Address.Cep,
            customer.Address.City,
            customer.Address.Street,
            customer.Address.District
        );
        await context.Customers.AddAsync(customerEfModel);
        await context.SaveChangesAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var customerEfModel = await context.Customers.FirstOrDefaultAsync(x => x.Id == id) ??
            throw new CustomerNotFoundException();

        var cpf = new Cpf(customerEfModel.Cpf);
        var address = new Address(customerEfModel.Cep, customerEfModel.City, customerEfModel.Street, customerEfModel.District);
        return new Customer(customerEfModel.Id, customerEfModel.Name, customerEfModel.Age, cpf, address);
    }
}