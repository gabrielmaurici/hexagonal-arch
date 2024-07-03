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
        var addressAwsS3ModelExpect = new AddressAwsS3Model("88999-999", "Cidade Teste", "Rua Teste", "Bairro Teste");
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
        Assert.Equal(addressAwsS3ModelExpect.City, addressAwsS3Model!.City);
        Assert.Equal(addressAwsS3ModelExpect.Street, addressAwsS3Model!.Street);
        Assert.Equal(addressAwsS3ModelExpect.District, addressAwsS3Model!.District);
        amazonS3Mock.Verify(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
    }

    [Fact(DisplayName = "Should return null when CEP not exists in bucket s3")]
    public async void CepNotExistsS3_WhenCepNotExistsInS3Bucket_ReturnNull()
    {
        var amazonS3Mock = new Mock<IAmazonS3>();
        amazonS3Mock.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception(""));
        var integrationAwsS3Service = new IntegrationAwsS3Service(amazonS3Mock.Object);

        var addressAwsS3Model = await integrationAwsS3Service.GetAddressByCepAsync("88999-999");
        
        Assert.Null(addressAwsS3Model);
        amazonS3Mock.Verify(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
    }
    
    [Fact(DisplayName = "Should create new Address in bucket s3")]
    public async void AddresIsValid_WhenAddressIsValid_CreateInBucketS3()
    {
        var addressAwsS3Model = new AddressAwsS3Model("88999-999", "Cidade Teste", "Rua Teste", "Bairro Teste");
        var amazonS3Mock = new Mock<IAmazonS3>();
        amazonS3Mock.Setup(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()));
        var integrationAwsS3Service = new IntegrationAwsS3Service(amazonS3Mock.Object);

        await integrationAwsS3Service.UploadCepAsync(addressAwsS3Model);
        
        amazonS3Mock.Verify(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
    }
}