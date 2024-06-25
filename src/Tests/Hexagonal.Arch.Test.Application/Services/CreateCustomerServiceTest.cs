using Hexagonal.Arch.Application;
using Hexagonal.Arch.Domain.Dtos;
using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Models;
using Hexagonal.Arch.Domain.Ports;
using Moq;

namespace Hexagonal.Arch.Test.Application.Services;

public class CreateCustomerServiceTest
{
    [Fact(DisplayName = "When data of customerDto is valid and AWS S3 returns an Address in cache, creates a new customer without call ViaCepApi")]
    public async void CustomerDtoValid_WhenCustomerDtoIsValidAndAwsS3ReturnAddressCache_CreatesNewCustomerWihtoutCallViaCepApi()
    {
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        customerRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Customer>()));
        var integrationAwsS3ServiceMock = new Mock<IIntegrationAwsS3Service>();
        integrationAwsS3ServiceMock.Setup(x => x.GetAddressByCepAsync(It.IsAny<string>()))
            .ReturnsAsync(new AddressAwsS3Model("88999-999", "Rua Teste", "Bairro Teste"));
        var integrationViaCepApiServiceMock = new Mock<IIntegrationViaCepApiService>();
        var customerDto = new CreateCustomerDto("Joãozinho", "999.999.999-99", 18, "88999-999");
        var createCustomerApplication = new CreateCustomerService(
            customerRepositoryMock.Object,
            integrationAwsS3ServiceMock.Object,
            integrationViaCepApiServiceMock.Object
        );

        await createCustomerApplication.Create(customerDto);

        integrationAwsS3ServiceMock.Verify(x => x.GetAddressByCepAsync(It.IsAny<string>()), Times.AtMostOnce());
        integrationViaCepApiServiceMock.Verify(x => x.GetAddressByCep(It.IsAny<string>()), Times.Never());
        customerRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.AtMostOnce());
    }
}