using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;

namespace Core.Services
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly ITransactionLogRepository _repository;

        public TransactionLogService(ITransactionLogRepository repository)
        {
            _repository = repository;
        }

        public ITransactionLog CreateEntity()
        {
           return _repository.CreateEntity();
        }

        public async Task SaveAsync(ITransactionLog log)
        {
            await _repository.SaveAsync(log);
        }
    }
}
