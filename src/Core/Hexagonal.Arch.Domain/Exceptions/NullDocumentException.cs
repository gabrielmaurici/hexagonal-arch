namespace Hexagonal.Arch.Domain.Exceptions;

public class NullDocumentException() : NullReferenceException("Document é um campo obrigatório")
{
}
