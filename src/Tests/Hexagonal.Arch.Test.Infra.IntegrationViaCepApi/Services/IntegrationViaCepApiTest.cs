using System.Net;
using System.Text.Json;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Infra.IntegrationViaCepApi;
using Moq;
using Moq.Protected;

namespace Hexagonal.Arch.Test.Infra.IntegrationViaCepApi;

public class IntegrationViaCepApiTest
{
    [Fact(DisplayName = "When CEP is valid, return status code 200 and string with address")]
    public async void CepIsVlaid_WhenCepIsValid_Returns200WithAddress()
    {
        var reponseExpect = new {
            Cep = "01001-000",
            Logradouro = "Praça da Sé",
            Bairro = "Sé",
            Localidade = "São Paulo",
        };
        var responseExpectSerialize = JsonSerializer.Serialize(reponseExpect);

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

        var address = await integrationViaCepApiService.GetAddressByCep("01001-000");

        Assert.Equal(responseExpectSerialize, address);
    }

    [Fact(DisplayName = "When CEP is invalid, return status code 400 and throw InvalidCepFormatException")]
    public async void CepIsInvalid_WhenCepIsInvalid_Returns400AndThrowInvalidCepFormatException()
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

    [Fact(DisplayName = "When CEP is invalid, return status code 400 and throw InvalidCepFormatException")]
    public async void ViaCepApiIsOut_WhenViaCepApiIsOut_Returns500AndThrowException()
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