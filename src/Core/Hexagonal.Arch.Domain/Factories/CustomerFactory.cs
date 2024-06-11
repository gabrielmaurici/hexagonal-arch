using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Domain.Factories;

public static class CustomerFactory
{
    public static Customer Create(string name, short? age, Cpf cpf, Address address) 
    {
        return new Customer(id: 0, name, age, cpf, address);
    }
    
}