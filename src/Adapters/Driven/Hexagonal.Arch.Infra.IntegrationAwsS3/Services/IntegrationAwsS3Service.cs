using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Hexagonal.Arch.Domain.Models;
using Hexagonal.Arch.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Hexagonal.Arch.Infra.IntegrationAwsS3.Services;

public class IntegrationAwsS3Service : IIntegrationAwsS3Service
{
    private readonly AmazonS3Client _client;
    private const string bucketName = "hexagonal-arch-via-cep";

    public IntegrationAwsS3Service(IConfiguration configuration)
    {
        var awsAccessKey = configuration.GetValue<string>("$aws_access_key");
        var awsSecretAccessKey = configuration.GetValue<string>("$aws_secret_access_key");

        var credentials = new BasicAWSCredentials(awsAccessKey, awsSecretAccessKey);
        _client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.APNortheast1);
    }

    public async Task<AddressAwsS3Model?> GetAddressByCepAsync(string cep)
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

        var addresAwsS3Model = JsonConvert.DeserializeObject<AddressAwsS3Model>(contet);
        return addresAwsS3Model;
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