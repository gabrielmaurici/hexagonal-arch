using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.ValueObjects;

namespace Hexagonal.Arch.Test.Domain.ValueObjects
{
    public class CpfTest
    {
        [Fact(DisplayName = "Return CPF when CPF is valid")]
        public void CpfValid_WhenCpfIsValid_ReturnCpf()
        {
            var document = "897.900.134-76";

            var cpf = new Cpf(document);

            Assert.Equal(document, cpf.Document);
        }

        [Fact(DisplayName = "Throw invalid CPF format exception when CPF is invalid")]
        public void CpfInvalid_WhenCpfIsInvalid_ThrowInvalidCpfFormatException()
        {
            var document = "897.90.134-6";

            Assert.Throws<InvalidCpfFormatException>(() => new Cpf(document));
        }
    }
}