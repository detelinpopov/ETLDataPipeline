using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Sql.Context;
using Sql.Mappers;

namespace Sql.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataPipelineDbContext _dbContext;

        public TransactionRepository(DataPipelineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(IEnumerable<ITransaction> transactions)
        {
            foreach (var transactionModel in transactions)
            {
                var dbTransaction = transactionModel.ToDbTransaction();   
                dbTransaction.CreatedDateTimeUtc = DateTime.UtcNow;
                _dbContext.Transactions.Add(dbTransaction);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
