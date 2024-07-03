using Hexagonal.Arch.Application;
using Hexagonal.Arch.Console.Menus;
using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Infra.Db;
using Hexagonal.Arch.Infra.IntegrationAwsS3;
using Hexagonal.Arch.Infra.IntegrationViaCepApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();


var servicesCollection = new ServiceCollection()
    .AddSingleton(configuration)
    .AddDb()
    .AddApplicationServices()
    .AddIntegrationViaCepApi(configuration)
    .AddIntegrationAwsS3(configuration);

var services = servicesCollection.BuildServiceProvider();

var createCustomerService = services.GetRequiredService<ICreateCustomerService>();
var getCustomerService = services.GetRequiredService<IGetCustomerService>();

var customersMenu = new CustomersMenu(createCustomerService, getCustomerService);
await customersMenu.Show();