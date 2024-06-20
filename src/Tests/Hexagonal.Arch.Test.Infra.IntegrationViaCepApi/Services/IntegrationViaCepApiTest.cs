using System.Net;
using System.Text.Json;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.Models;
using Hexagonal.Arch.Infra.IntegrationViaCepApi;
using Moq;
using Moq.Protected;

namespace Hexagonal.Arch.Test.Infra.IntegrationViaCepApi;

public class IntegrationViaCepApiTest
{
    [Fact(DisplayName = "When CEP is valid, return status code 200 and string with address")]
    public async void CepIsVlaid_WhenCepIsValid_Returns200WithAddress()
    {
        var responseAddressAwsModel = new AddressAwsS3Model(
            Cep: "01001-000",
            Logradouro: "Praça da Sé",
            Bairro: "Sé" 
        );
        var responseExpectSerialize = JsonSerializer.Serialize(responseAddressAwsModel);

        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseExpectSerialize)
            });

        var client = new HttpClient(mockMessageHandler.Object) 
        { 
            BaseAddress = new Uri("https://viacep.com.br/")
        };
        
        var integrationViaCepApiService = new IntegrationViaCepApiService(client);
        var addressAwsS3Model = await integrationViaCepApiService.GetAddressByCep("01001-000");

        Assert.Equal(responseAddressAwsModel, addressAwsS3Model);
    }

    [Fact(DisplayName = "When CEP is invalid, return status code 400 and throws InvalidCepFormatException")]
    public async void CepIsInvalid_WhenCepIsInvalid_Returns400AndThrowsInvalidCepFormatException()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.BadRequest,
            });
        var client = new HttpClient(mockMessageHandler.Object) 
        { 
            BaseAddress = new Uri("https://viacep.com.br/")
        };
        var integrationViaCepApiService = new IntegrationViaCepApiService(client);

        await Assert.ThrowsAsync<InvalidCepFormatException>(() => integrationViaCepApiService.GetAddressByCep("01000"));
    }

    [Fact(DisplayName = "When ViaCepApi is down, return status code 500 and throws Exception")]
    public async void ViaCepApiIsDown_WhenViaCepApiIsDown_Returns500AndThrowsException()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.InternalServerError,
            });
        var client = new HttpClient(mockMessageHandler.Object) 
        { 
            BaseAddress = new Uri("https://viacep.com.br/")
        };
        var integrationViaCepApiService = new IntegrationViaCepApiService(client);

        var exception = await Assert.ThrowsAsync<Exception>(() => integrationViaCepApiService.GetAddressByCep("01000"));
        Assert.Equal("Erro ao consultar CEP do cliente", exception.Message);
    }
}