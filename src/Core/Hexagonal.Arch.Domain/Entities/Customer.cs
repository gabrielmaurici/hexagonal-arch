using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Domain.Entities;

public class Customer
{
    public Customer(string name, short? age, Cpf document, Address address)
    {
        Name = name;
        Age = age;
        Document = document;
        Address = address;
        Validate();
    }

    public string Name { get; private set; } = null!;
    public short? Age { get; set; }
    public Cpf Document { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new NullNameException();

        if (Document == null)
            throw new NullDocumentException();

        if (Address == null)
            throw new NullAddressException();
    }
}