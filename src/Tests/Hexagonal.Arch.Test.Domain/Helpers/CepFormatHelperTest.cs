using Hexagonal.Arch.Domain.Exceptions;
using Hexagonal.Arch.Domain.Helpers;

namespace Hexagonal.Arch.Test.Domain.Helpers;

public class CepFormatHelperTest
{
    [Fact(DisplayName = "When format of CEP is invalid, throw InvalidCepFormatException")]
    public void CepFormatInvalid_WhenCepFormatIsInvalid_ThrowInvalidCepFormatException()
    {
        var cepInvalid = "11111111";

        Assert.Throws<InvalidCepFormatException>(() => CepFormatHelper.Validate(cepInvalid));
    }
}