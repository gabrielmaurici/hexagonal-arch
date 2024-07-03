using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Test.Domain.ValueObjects
{
    public class AddressTest
    {
        [Fact(DisplayName = "Return Address when Addres is valid")]
        public void AddressValid_WhenAddressIsValid_ReturnAddress()
        {
            var cep = "88999-999";
            var city = "Cidade Teste";
            var street = "Rua Teste";
            var district = "Bairro Teste";

            var address = new Address(cep, city, street, district);

            Assert.Equal(cep, address.Cep);
            Assert.Equal(city, address.City);
            Assert.Equal(street, address.Street);
            Assert.Equal(district, address.District);
        }

        [Fact(DisplayName = "Throw invalid CEP format exception when CEP is invalid")]
        public void CepInvalid_WhenCepIsInvalid_ThrowInvalidCepFormatException()
        {
            var cep = "88999-9";

            Assert.Throws<InvalidCepFormatException>(() => new Address(cep));
        }

        [Fact(DisplayName = "Throw null CEP exception when CEP is null or empty")]
        public void CepNullorEmpty_WhenCepIsNullOrEmpty_ThrowInvalidCepFormatException()
        {
            string? cepNull = null;
            string cepEmpty = string.Empty;

            Assert.Throws<NullCepException>(() => new Address(cepNull!));
            Assert.Throws<NullCepException>(() => new Address(cepEmpty));
        }
    }
}