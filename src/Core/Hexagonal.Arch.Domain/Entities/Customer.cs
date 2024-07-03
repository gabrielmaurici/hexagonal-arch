using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Domain.Entities;

public class Customer
{
    public Customer(int id,string name, short? age, Cpf cpf, Address address)
    {
        Id = id;
        Name = name;
        Age = age;
        Cpf = cpf;
        Address = address;
        Validate();
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public short? Age { get; set; }
    public Cpf Cpf { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new NullNameException();

        if (Cpf == null)
            throw new NullCpfException();

        if (Address == null)
            throw new NullAddressException();
    }

    public override string ToString() 
    {
        return $"Nome: {Name}" +
            $"\nIdade: {Age}"+
            $"\nCPF: {Cpf.Document}" +
            $"\nCEP: {Address.Cep}"+
            $"\nCidade: {Address.City}"+
            $"\nRua: {Address.Street}"+
            $"\nBairro: {Address.District}";
    }
}