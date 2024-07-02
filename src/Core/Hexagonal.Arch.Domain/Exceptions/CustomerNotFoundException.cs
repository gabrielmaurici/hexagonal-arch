namespace Hexagonal.Arch.Domain.Exceptions;

public class CustomerNotFoundException() : KeyNotFoundException("Cliente não encontrado")
{
}