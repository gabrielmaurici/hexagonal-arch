using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.Helpers;

namespace Hexagonal.Arch.Domain.ValueObjects;

public class Address
{
    public Address(string cep, string? city = null, string? street = null, string? district = null)
    {
        Cep = cep;
        City = city;
        Street = street;
        District = district;
        Validate();
    }

    public string Cep { get; set; } = null!;
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? District { get; set; }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Cep))
            throw new NullCepException();

        CepFormatHelper.Validate(Cep);
    }
}