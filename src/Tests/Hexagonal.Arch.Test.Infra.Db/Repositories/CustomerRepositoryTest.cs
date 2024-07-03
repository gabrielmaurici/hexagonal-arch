using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.Factories;
using Hexagonal.Arch.Domain.ValueObjects;
using Hexagonal.Arch.Infra.Db.Context;
using Hexagonal.Arch.Infra.Db.Models;
using Hexagonal.Arch.Infra.Db.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hexagonal.Arch.Test.Infra.Db.Repositories;

public class CustomerRepositoryTest
{
    [Fact(DisplayName = "Add new Customer in db when customer is valid")]
    public async void CustomerValid_WhenCustomerIsValid_AddInRepository()
    {
        var idCustomerInDb = 1;
        var cpf = new Cpf("999.999.999-99");
        var address = new Address("88999-999", "Cidade teste", "Rua teste", "Bairro teste");
        var customer = CustomerFactory.Create("Joãozinho", 18, cpf, address);
        var contextMock = GetContextMock();
        var customerRepositoryMock = GetCustomerRepositoryMock(contextMock);

        await customerRepositoryMock.CreateAsync(customer);
        var customerEfModelDb = await contextMock.Customers.FirstAsync();

        Assert.Equal(idCustomerInDb, customerEfModelDb.Id);
        Assert.Equal(customer.Name, customerEfModelDb.Name);
        Assert.Equal(customer.Age, customerEfModelDb.Age);
        Assert.Equal(customer.Cpf.Document, customerEfModelDb.Cpf);
        Assert.Equal(customer.Address.Cep, customerEfModelDb.Cep);
        Assert.Equal(customer.Address.City, customerEfModelDb.City);
        Assert.Equal(customer.Address.Street, customerEfModelDb.Street);
        Assert.Equal(customer.Address.District, customerEfModelDb.District);
    }

    [Fact(DisplayName = "Get customer by id when customer exists in db")]
    public async void CustomerExists_WhenCustomerExistsInDb_GetById()
    {
        var customerEfModel = new CustomerEfModel(0, "Joãozinho", 18, "999.999.999-99", "88999-999", "Cidade teste", "Rua teste", "Bairro teste");
        var contextMock = GetContextMock();
        var customerRepositoryMock = GetCustomerRepositoryMock(contextMock);

        await contextMock.Customers.AddAsync(customerEfModel);
        await contextMock.SaveChangesAsync();
        var customerDb = await customerRepositoryMock.GetByIdAsync(customerEfModel.Id);

        Assert.IsType<Customer>(customerDb);
        Assert.Equal(customerEfModel.Id, customerDb.Id);
        Assert.Equal(customerEfModel.Name, customerDb.Name);
        Assert.Equal(customerEfModel.Age, customerDb.Age);
        Assert.Equal(customerEfModel.Cpf, customerDb.Cpf.Document);
        Assert.Equal(customerEfModel.Cep, customerDb.Address.Cep);
        Assert.Equal(customerEfModel.City, customerDb.Address.City);
        Assert.Equal(customerEfModel.Street, customerDb.Address.Street);
        Assert.Equal(customerEfModel.District, customerDb.Address.District);
    }

    [Fact(DisplayName = "Throw CustomerNotFoundException when customer not exists in db")]
    public async void CustomerNotExists_WhenCustomerNotExistsInDb_ThrowCustomerNotFoundException()
    {
        var contextMock = GetContextMock();
        var customerRepositoryMock = GetCustomerRepositoryMock(contextMock);

        await Assert.ThrowsAsync<CustomerNotFoundException>(async () => await customerRepositoryMock.GetByIdAsync(0));
    }

    public static HexagonalContext GetContextMock() 
    {
        var options = new DbContextOptionsBuilder<HexagonalContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;

        var mockContext = new HexagonalContext(options);
        return mockContext;
    }

    public static CustomerRepository GetCustomerRepositoryMock(HexagonalContext context) 
    {
        var repository = new CustomerRepository(context);
        return repository;
    }
}