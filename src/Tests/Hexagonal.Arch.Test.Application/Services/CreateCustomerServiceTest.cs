using Hexagonal.Arch.Application;
using Hexagonal.Arch.Domain.Dtos;
using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Exceptions;
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

        await createCustomerApplication.CreateAsync(customerDto);

        integrationAwsS3ServiceMock.Verify(x => x.GetAddressByCepAsync(It.IsAny<string>()), Times.AtMostOnce());
        integrationAwsS3ServiceMock.Verify(x => x.UploadCepAsync(It.IsAny<AddressAwsS3Model>()), Times.Never());
        integrationViaCepApiServiceMock.Verify(x => x.GetAddressByCepAsync(It.IsAny<string>()), Times.Never());
        customerRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.AtMostOnce());
    }

    [Fact(DisplayName = "When the customerDto data is valid and AWS S3 does not have a cached address, it creates a new customer by searching for the address in ViaCepApi and upload it to AWS S3")]
    public async void CustomerDtoValid_WhenCustomerDtoIsValidAndAwsS3DoesNotHaveAddressCache_CreatesNewCustomerSearchingViaCepApiAddressAndUploadAwsS3()
    {
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var integrationAwsS3ServiceMock = new Mock<IIntegrationAwsS3Service>();
        integrationAwsS3ServiceMock.Setup(x => x.GetAddressByCepAsync(It.IsAny<string>()))
            .ReturnsAsync((AddressAwsS3Model?)null);
        var integrationViaCepApiServiceMock = new Mock<IIntegrationViaCepApiService>();
        integrationViaCepApiServiceMock.Setup(x => x.GetAddressByCepAsync(It.IsAny<string>()))
            .ReturnsAsync(new AddressViaCepModel { Cep = "88999-999", Logradouro = "Rua Teste", Bairro = "Bairro Teste"});
        var customerDto = new CreateCustomerDto("Joãozinho", "999.999.999-99", 18, "88999-999");
        var createCustomerApplication = new CreateCustomerService(
            customerRepositoryMock.Object,
            integrationAwsS3ServiceMock.Object,
            integrationViaCepApiServiceMock.Object
        );

        await createCustomerApplication.CreateAsync(customerDto);

        integrationAwsS3ServiceMock.Verify(x => x.GetAddressByCepAsync(It.IsAny<string>()), Times.AtMostOnce());
        integrationViaCepApiServiceMock.Verify(x => x.GetAddressByCepAsync(It.IsAny<string>()), Times.AtMostOnce());
        integrationAwsS3ServiceMock.Verify(x => x.UploadCepAsync(It.IsAny<AddressAwsS3Model>()), Times.AtMostOnce());
        customerRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.AtMostOnce());
    }

    [Fact(DisplayName = "When the CEP of customerDto is invalid thows InvalidCepFormatException")]
    public async void CustomerDtoCepInvalid_WhenCustomerDtoCepisInvalid_ThrowsInvalidCepFormatException()
    {
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var integrationAwsS3ServiceMock = new Mock<IIntegrationAwsS3Service>();
        var integrationViaCepApiServiceMock = new Mock<IIntegrationViaCepApiService>();
        var customerDto = new CreateCustomerDto("Joãozinho", "999.999.999-99", 18, "111111");
        var createCustomerApplication = new CreateCustomerService(
            customerRepositoryMock.Object,
            integrationAwsS3ServiceMock.Object,
            integrationViaCepApiServiceMock.Object
        );

        await Assert.ThrowsAsync<InvalidCepFormatException>(async () => await createCustomerApplication.CreateAsync(customerDto));
        integrationAwsS3ServiceMock.Verify(x => x.GetAddressByCepAsync(It.IsAny<string>()), Times.Never());
        integrationViaCepApiServiceMock.Verify(x => x.GetAddressByCepAsync(It.IsAny<string>()), Times.Never());
        integrationAwsS3ServiceMock.Verify(x => x.UploadCepAsync(It.IsAny<AddressAwsS3Model>()), Times.Never());
        customerRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never());
    }
}