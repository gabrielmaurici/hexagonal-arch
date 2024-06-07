namespace Hexagonal.Arch.Domain.Exceptions;

public class NullNameException() : NullReferenceException("Name é um campo obrigatório")
{
}
