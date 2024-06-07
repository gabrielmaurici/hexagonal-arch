namespace Hexagonal.Arch.Domain.Exceptions;

public class NullCpfException() : NullReferenceException("Cpf é um campo obrigatório")
{
}