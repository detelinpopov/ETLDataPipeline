using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Sql.Context;
using Sql.Entities;
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

            var paymentDetailsToSave = transactionsToSave.Where(t => t.PaymentDetails != null).Select(t => t.PaymentDetails).ToList();

            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                // Use BulkMergeAsync to upsert records
                await _dbContext.BulkMergeAsync(paymentDetailsToSave, options =>
                {
                    options.ColumnPrimaryKeyExpression = paymentDetails => paymentDetails.Id;
                    options.AllowDuplicateKeys = false;
                });

                await _dbContext.BulkMergeAsync(transactionsToSave, options =>
                {
                    options.ColumnPrimaryKeyExpression = transaction => transaction.Id;
                    options.AllowDuplicateKeys = false;
                    //options.IncludeGraph = true;
                    //options.IncludeGraphOperationBuilder = operation =>
                    //{
                    //    if (operation is BulkOperation<PaymentDetails>)
                    //    {
                    //        var bulk = (BulkOperation<PaymentDetails>)operation;
                    //        bulk.ColumnPrimaryKeyExpression = d => d.Id;
                    //        bulk.InsertIfNotExists = true;                       
                    //    }                  
                    //};
                });

                dbContextTransaction.Commit();
            }
        }
    }
}
