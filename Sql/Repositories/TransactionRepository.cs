using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Sql.Context;
using Sql.Entities;
using Sql.Mappers;
using System.Collections.Generic;

namespace Sql.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataPipelineDbContext _dbContext;

        public TransactionRepository(DataPipelineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransaction CreateEntity()
        {
            return new Transaction();
        }

        public async Task SaveAsync(IEnumerable<ITransaction> transactions)
        {
            var transactionsToSave = transactions.ToListOfDbTransactions();

            await _dbContext.BulkMergeAsync(transactionsToSave, options =>
            {
                options.ColumnPrimaryKeyExpression = transaction => transaction.Id;
                options.AllowDuplicateKeys = false;
            });          
        }
    }
}
