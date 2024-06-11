using Hexagonal.Arch.Domain.Entities;
using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.ValueObjects;

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

            var name = "Joãozinho";
            short? age = 18;
            var customer = new Customer(name, age, cpf, address);
    
            Assert.Equal(document, customer.Cpf.Document);

            Assert.Equal(cep, customer.Address.Cep);
            Assert.Equal(street, customer.Address.Street);
            Assert.Equal(district, customer.Address.District);

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
            
            string? nameNull = null;
            var nameEmpty = string.Empty;
            short? age = 18;
            
            Assert.Throws<NullNameException>(() => new Customer(nameNull!, age, cpf, address!));
            Assert.Throws<NullNameException>(() => new Customer(nameEmpty, age, cpf, address!));
        }

        [Fact(DisplayName = "Throw null Cpf exception when Cpf is null")]
        public void CpfIsNull_WhenCpfIsNull_ThrowNullCpfException()
        {
            Cpf? cpf = null;
            
            var cep = "88999-999";
            var street = "Rua Teste";
            var district = "Bairro Teste";
            var address = new Address(cep, street, district);
            
            var name = "Joãozinho";
            short? age = 18;
            
            Assert.Throws<NullCpfException>(() => new Customer(name, age, cpf!, address!));
        }

        [Fact(DisplayName = "Throw null Address exception when Addres is null")]
        public void AddressNull_WhenAdressIsNull_ThrowNullAddressException()
        {
            var name = "Joãozinho";
            short? age = 18;
            var cpf = new Cpf("999.999.999-99");
            Address? address = null;

            Assert.Throws<NullAddressException>(() => new Customer(name, age, cpf, address!));
        }
    }
}