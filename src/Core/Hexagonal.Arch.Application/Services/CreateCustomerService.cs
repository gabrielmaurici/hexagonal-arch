using Hexagonal.Arch.Domain.Dtos;
using Hexagonal.Arch.Domain.Helpers;
using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Application;

public class CreateCustomerService(
    ICustomerRepository customerRepository,
    IIntegrationAwsS3Service integrationAwsS3Service,
    IIntegrationViaCepApiService integrationViaCepApiService) : ICreateCustomerService
{
    public async Task Create(CreateCustomerDto customer)
    {
        CepFormatHelper.Validate(customer.Cep);
        Address address;

        var addressS3 = await integrationAwsS3Service.GetAddressByCep(customer.Cep);
        if (addressS3 != null) 
        {
            // address = new(addressS3.Cep, addressS3.Logradouro, addressS3.Bairro);
        }
        else
        {
            var addressViaCep = await integrationViaCepApiService.GetAddressByCep(customer.Cep);

        }
    }
}
