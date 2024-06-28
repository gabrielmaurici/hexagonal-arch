using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using Hexagonal.Arch.Domain.Models;
using Hexagonal.Arch.Domain.Ports;
using Newtonsoft.Json;

namespace Hexagonal.Arch.Infra.IntegrationAwsS3.Services;

public class IntegrationAwsS3Service(IAmazonS3 _client) : IIntegrationAwsS3Service
{
    private const string bucketName = "hexagonal-arch-ceps";

    public async Task<AddressAwsS3Model?> GetAddressByCepAsync(string cep)
    {
        try
        {            
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = cep            
            };

            using var response = await _client.GetObjectAsync(request);
            using var streamReader = new StreamReader(response.ResponseStream);
            var contet = await streamReader.ReadToEndAsync();

            response.Dispose();
            streamReader.Dispose();

            var addressAwsS3Model = JsonConvert.DeserializeObject<AddressAwsS3Model>(contet);
            return addressAwsS3Model;
        }
        catch (AmazonS3Exception)
        {
            return null;
        }
    }

    public async Task UploadCepAsync(AddressAwsS3Model address)
    {
        var jsonAddress = JsonConvert.SerializeObject(address);

        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = address.Cep,
            InputStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonAddress))
        };

        await _client.PutObjectAsync(request);
    }
}