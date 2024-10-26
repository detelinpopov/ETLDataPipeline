using EFCore.BulkExtensions;
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
            var dbTransactions = transactions.ToListOfDbTransactions();         
            await _dbContext.BulkInsertOrUpdateAsync(dbTransactions, o => o.IncludeGraph = true);
        }
    }
}
