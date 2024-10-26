using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Sql.Context;
using Sql.Entities;
using Sql.Mappers;
using Z.BulkOperations;

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

        public IPaymentDetails CreatePaymentDetailsEntity()
        {
            return new PaymentDetails();
        }

        public async Task SaveAsync(IEnumerable<ITransaction> transactions)
        {
            var transactionsToSave = transactions.ToListOfDbTransactions();

            await _dbContext.BulkMergeAsync(transactionsToSave, options =>
            {
                options.ColumnPrimaryKeyExpression = transaction => transaction.Id;
                options.AllowDuplicateKeys = false;
                options.IncludeGraph = true;
                options.IncludeGraphOperationBuilder = operation =>
                {
                    if (operation is BulkOperation<PaymentDetails>)
                    {
                        var bulk = (BulkOperation<PaymentDetails>)operation;
                        bulk.ColumnPrimaryKeyExpression = d => d.Id;
                    }                  
                };
            });
        }
    }
}
