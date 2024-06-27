using Hexagonal.Arch.Application.Services;
using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Domain.ValueObjects;
using Moq;

namespace Hexagonal.Arch.Test.Application.Services;

public class GetCustomerServiceTest
{
    [Fact(DisplayName = "Should return a customer when id exists in db")]
    public async void CustomerExists_WhenCustomerExistsInDb_ReturnCustomer()
    {
        var id = 1;
        var cpf = new Cpf("999.999.999-99");
        var address = new Address("88999-999", "Rua Teste", "Bairro Teste");
        var customerExpect = new Customer(id, "Joãozinho", 20, cpf, address);
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(x => x.GetByIdAsync(id))
            .ReturnsAsync(customerExpect);
        var getCustomerService = new GetCustomerService(customerRepositoryMock.Object);

        var customer = await getCustomerService.GetAsync(id);

        Assert.Equal(customerExpect.Id, customer.Id);
        Assert.Equal(customerExpect.Name, customer.Name);
        Assert.Equal(customerExpect.Cpf.Document, customer.Cpf.Document);
        Assert.Equal(customerExpect.Address.Cep, customer.Address.Cep);
        Assert.Equal(customerExpect.Address.Street, customer.Address.Street);
        Assert.Equal(customerExpect.Address.District, customer.Address.District);
        customerRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.AtMostOnce());
    }
}