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
       
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
