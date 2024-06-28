using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Infra.IntegrationAwsS3.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Arch.Infra.IntegrationAwsS3;

public static class IntegrationAwsS3Extensions
{
    public static IServiceCollection AddIntegrationAwsS3(this IServiceCollection services, IConfiguration configuration)
    {
        var awsAccessKey = configuration.GetValue<string>("aws_access_key");
        var awsSecretAccessKey = configuration.GetValue<string>("aws_secret_access_key");
        var credentials = new BasicAWSCredentials(awsAccessKey, awsSecretAccessKey);
        AWSOptions awsOptions = new()
        {
            Region = Amazon.RegionEndpoint.USEast1,
            Credentials = credentials,
        };
        services.AddAWSService<IAmazonS3>(awsOptions);
        
        services.AddScoped<IIntegrationAwsS3Service, IntegrationAwsS3Service>();

        return services;
    } 
}