using Microsoft.EntityFrameworkCore;
using Sql.Entities;

namespace Sql.Context
{
    public class DataPipelineDbContext : DbContext
    {
        public DataPipelineDbContext(DbContextOptions<DataPipelineDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {              
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }         
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}
