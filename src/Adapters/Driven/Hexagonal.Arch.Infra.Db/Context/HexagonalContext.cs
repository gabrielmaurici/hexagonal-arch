using System.Reflection;
using Hexagonal.Arch.Infra.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Hexagonal.Arch.Infra.Db.Context
{
    public class HexagonalContext(DbContextOptions<HexagonalContext> options) : DbContext(options)
    {
        public DbSet<CustomerEfModel> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}