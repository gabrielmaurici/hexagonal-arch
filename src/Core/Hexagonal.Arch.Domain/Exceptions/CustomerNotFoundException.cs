namespace Hexagonal.Arch.Domain.Exceptions;

public class CustomerNotFoundException() : NullReferenceException("Cliente não encontrados")
{
}