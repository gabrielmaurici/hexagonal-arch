using System.Text.RegularExpressions;
using Hexagonal.Arch.Domain.Exceptions;

namespace Hexagonal.Arch.Domain.Helpers;

public static class CepFormatHelper
{
    public static void Validate(string cep) 
    {
        string patternCepFormat = @"^\d{5}-\d{3}$";
        Regex regex = new(patternCepFormat);

        var formatValid = regex.IsMatch(cep);
        if (!formatValid)
            throw new InvalidCepFormatException();
    }
}