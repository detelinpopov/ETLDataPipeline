using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface ITransactionLogRepository
    {
        public ITransactionLog CreateEntity();
        
        Task SaveAsync(ITransactionLog log);
    }
}
