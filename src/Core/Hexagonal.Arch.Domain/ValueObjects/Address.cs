
using System.Text.RegularExpressions;
using Hexagonal.Arch.Domain.Exceptions;

namespace Hexagonal.Arch.Domain.ValueObjects;

public class Address
{
    public Address(string cep, string? street, string? district)
    {
        Cep = cep;
        Street = street;
        District = district;
        Validate();
    }

    public string Cep { get; set; } = null!;
    public string? Street { get; set; }
    public string? District { get; set; }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Cep))
            throw new NullCepException();

        string patternCepFormat = @"^\d{5}-\d{3}$";
        Regex regex = new(patternCepFormat);

        var formatValid = regex.IsMatch(Cep);
        if (!formatValid)
            throw new InvalidCepFormatException();
    }
}