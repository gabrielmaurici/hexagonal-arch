namespace Hexagonal.Arch.Domain.Exceptions;

public class NullCepException() : NullReferenceException("CEP é um campo obrigatório")
{
}