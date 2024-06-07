using System.Text.RegularExpressions;
using Hexagonal.Arch.Domain.Exceptions;

namespace Hexagonal.Arch.Domain.ValueObjects;

public class Cpf
{
    public Cpf(string document) 
    {
        Document = document;
        Validate();
    }

    public string Document { get; set; } = null!;

    private void Validate() 
    {
        if (string.IsNullOrWhiteSpace(Document))
            throw new NullCpfException();

        string patternFormatDocument = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";
        Regex regex = new(patternFormatDocument);
        
        bool formatValid = regex.IsMatch(Document);
        if (!formatValid)
            throw new InvalidCpfFormatException();
    }
}