namespace Hexagonal.Arch.Infra.Db.Models
{
    public class CustomerEfModel(int id, string name, short? age, string cpf, string cep, string? street, string? district)
    {
        public int Id {get; set;} = id;
        public string Name { get; set; } = name;
        public short? Age { get; set; } = age;
        public string Cpf { get; set; } = cpf;
        public string Cep { get; set; } = cep;
        public string? Street { get; set; } = street;
        public string? District { get; set; } = district;
    }
}