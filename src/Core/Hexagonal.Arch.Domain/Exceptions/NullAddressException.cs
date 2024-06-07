namespace Hexagonal.Arch.Domain.Exceptions;

public class NullAddressException() : NullReferenceException("Address é um campo obrigatório")
{
}