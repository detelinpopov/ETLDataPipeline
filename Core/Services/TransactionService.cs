using Interfaces.Core.Services;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;

namespace Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public ITransaction CreateEntity()
        {
            return _repository.CreateEntity();
        }

        public ICustomer CreateCustomerEntity()
        {
            return _repository.CreateCustomerEntity();
        }

        public async Task SaveAsync(IEnumerable<ITransaction> transactions)
        {
            await _repository.SaveAsync(transactions);
        }
    }
}
