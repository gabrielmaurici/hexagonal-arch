using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using Hexagonal.Arch.Domain.Models;
using Hexagonal.Arch.Infra.IntegrationAwsS3.Services;
using Moq;
using Newtonsoft.Json;

namespace Hexagaonl.Arch.Test.Infra.IntegrationAwsS3;

public class IntegrationAwsS3ServiceTest
{
    [Fact(DisplayName = "Should return a Address when CEP exists in bucket s3")]
    public async void CepExistsS3_WhenCepExistsInS3Bucket_ReturnAddress()
    {
        var addressAwsS3ModelExpect = new AddressAwsS3Model("88999-999", "Rua Teste", "Bairro Teste");
        var jsonAddress = JsonConvert.SerializeObject(addressAwsS3ModelExpect);
        var responseExpect = new GetObjectResponse
        {
            ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonAddress))
        };
        var amazonS3Mock = new Mock<IAmazonS3>();
        amazonS3Mock.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(responseExpect);
        var integrationAwsS3Service = new IntegrationAwsS3Service(amazonS3Mock.Object);

        var addressAwsS3Model = await integrationAwsS3Service.GetAddressByCepAsync("88999-999");
        
        Assert.Equal(addressAwsS3ModelExpect.Cep, addressAwsS3Model!.Cep);
        Assert.Equal(addressAwsS3ModelExpect.Street, addressAwsS3Model!.Street);
        Assert.Equal(addressAwsS3ModelExpect.District, addressAwsS3Model!.District);
        amazonS3Mock.Verify(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
    }
}