using Hexagonal.Arch.Infra.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hexagonal.Arch.Infra.Db.Mappings;

public class CustomerEfModelMapping : IEntityTypeConfiguration<CustomerEfModel>
{
    public void Configure(EntityTypeBuilder<CustomerEfModel> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar");

        builder.Property(x => x.Age)
            .HasColumnName("Age")
            .HasColumnType("smallint");

        builder.Property(x => x.Cpf)
            .HasColumnName("Cpf")
            .HasColumnType("varchar");

        builder.Property(x => x.Cep)
            .HasColumnName("Cep")
            .HasColumnType("varchar");

        builder.Property(x => x.Street)
            .HasColumnName("Street")
            .HasColumnType("varchar");

        builder.Property(x => x.District)
            .HasColumnName("District")
            .HasColumnType("varchar");
    }
}