using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Sql.Context;
using Sql.Entities;
using Sql.Mappers;

namespace Sql.Repositories
{
    public class TransactionLogRepository : ITransactionLogRepository
    {
        private readonly DataPipelineDbContext _dbContext;

        public TransactionLogRepository(DataPipelineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransactionLog CreateEntity()
        {
            return new TransactionLog();
        }

        public async Task SaveAsync(ITransactionLog log)
        {
            var dbLog = log.ToDbTransactionLog();
           _dbContext.TransactionLog.Add(dbLog);
           await _dbContext.SaveChangesAsync();
        }
    }
}
