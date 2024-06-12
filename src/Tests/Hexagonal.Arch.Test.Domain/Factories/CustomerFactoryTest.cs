using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Factories;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Test.Domain.Factories;

public class CustomerFactoryTest
{
    [Fact(DisplayName = "Return new Customer when create CustomerFactory valid")]
    public void CustomerFactoryValid_WhenCustomerFactoryValid_ReturnNewCustomer() 
    {
        var id = 0;
        var name = "Joãozinho";
        short? age = 18;
        var cpf = new Cpf("999.999.999-99");
        var address = new Address("88999-999", "Rua Teste", "Bairro Teste");

        var customer = CustomerFactory.Create(name, age, cpf, address);

        Assert.IsType<Customer>(customer);
        Assert.Equal(id, customer.Id);
        Assert.Equal(name, customer.Name);
        Assert.Equal(age, customer.Age);
        Assert.Equal(cpf, customer.Cpf);
        Assert.Equal(address, customer.Address);
    }    
}