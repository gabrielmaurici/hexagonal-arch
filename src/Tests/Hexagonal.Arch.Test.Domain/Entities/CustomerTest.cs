using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.ValueObjects;
using Microsoft.VisualBasic;

namespace Hexagonal.Arch.Test.Domain.Entities
{
    public class CustomerTest
    {
        [Fact(DisplayName = "Return Customer when Customer is valid")]
        public void CustomerValid_WhenCustomerIsValid_ReturnCustomer()
        {
            var document = "999.999.999-99";
            var cpf = new Cpf(document);

            var cep = "88999-999";
            var street = "Rua Teste";
            var district = "Bairro Teste";
            var address = new Address(cep, street, district);

            var id = 0;
            var name = "Joãozinho";
            short? age = 18;
            var customer = new Customer(id, name, age, cpf, address);
    
            Assert.Equal(document, customer.Cpf.Document);
            Assert.Equal(cep, customer.Address.Cep);
            Assert.Equal(street, customer.Address.Street);
            Assert.Equal(district, customer.Address.District);
            Assert.Equal(id, customer.Id);
            Assert.Equal(name, customer.Name);
            Assert.Equal(age, customer.Age);
        }

        [Fact(DisplayName = "Throw null Name exception when Name is null or empty")]
        public void NameIsNullOrEmpty_WhenNameIsNullOrEmpty_ThrowNullNameException()
        {
            var document = "999.999.999-99";
            var cpf = new Cpf(document);
            
            var cep = "88999-999";
            var street = "Rua Teste";
            var district = "Bairro Teste";
            var address = new Address(cep, street, district);
            
            var id = 0;
            string? nameNull = null;
            var nameEmpty = string.Empty;
            short? age = 18;
            
            Assert.Throws<NullNameException>(() => new Customer(id, nameNull!, age, cpf, address!));
            Assert.Throws<NullNameException>(() => new Customer(id, nameEmpty, age, cpf, address!));
        }

        [Fact(DisplayName = "Throw null Cpf exception when Cpf is null")]
        public void CpfIsNull_WhenCpfIsNull_ThrowNullCpfException()
        {
            Cpf? cpf = null;
            
            var cep = "88999-999";
            var street = "Rua Teste";
            var district = "Bairro Teste";
            var address = new Address(cep, street, district);
            
            var id = 0;
            var name = "Joãozinho";
            short? age = 18;
            
            Assert.Throws<NullCpfException>(() => new Customer(id, name, age, cpf!, address!));
        }

        [Fact(DisplayName = "Throw null Address exception when Addres is null")]
        public void AddressNull_WhenAdressIsNull_ThrowNullAddressException()
        {
            var cpf = new Cpf("999.999.999-99");

            Address? address = null;

            var id = 0;
            var name = "Joãozinho";
            short? age = 18;

            Assert.Throws<NullAddressException>(() => new Customer(id, name, age, cpf, address!));
        }
    }
}