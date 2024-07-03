namespace Hexagonal.Arch.Domain.Models;

public class AddressViaCepModel 
{
    public string? Erro { get; set; }
    public string? Cep { get; set; }
    public string? Localidade { get; set; }
    public string? Logradouro { get; set; }
    public string? Bairro { get; set; }
}