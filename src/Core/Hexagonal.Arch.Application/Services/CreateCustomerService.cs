using Hexagonal.Arch.Domain.Dtos;
using Hexagonal.Arch.Domain.Factories;
using Hexagonal.Arch.Domain.Helpers;
using Hexagonal.Arch.Domain.Models;
using Hexagonal.Arch.Domain.Ports;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Application;

public class CreateCustomerService(
    ICustomerRepository customerRepository,
    IIntegrationAwsS3Service integrationAwsS3Service,
    IIntegrationViaCepApiService integrationViaCepApiService) : ICreateCustomerService
{
    public async Task Create(CreateCustomerDto customerDto)
    {
        CepFormatHelper.Validate(customerDto.Cep);
        Address address;

        var addressAwsS3Cache = await integrationAwsS3Service.GetAddressByCepAsync(customerDto.Cep);
        if (addressAwsS3Cache == null) 
        {
            var addressViaCep = await integrationViaCepApiService.GetAddressByCepAsync(customerDto.Cep);
            var adressAwsS3Model = new AddressAwsS3Model(addressViaCep.Cep!, addressViaCep.Logradouro!, addressViaCep.Bairro!);
            await integrationAwsS3Service.UploadCepAsync(adressAwsS3Model);

            address = new(addressViaCep.Cep!, addressViaCep.Logradouro, addressViaCep.Bairro);
        }
        else
        {
            address = new(addressAwsS3Cache.Cep, addressAwsS3Cache.Street, addressAwsS3Cache.District);
        }

        var cpf = new Cpf(customerDto.Cpf);

        var customer = CustomerFactory.Create(customerDto.Name, customerDto.Age, cpf, address);
        await customerRepository.CreateAsync(customer);
    }
}
