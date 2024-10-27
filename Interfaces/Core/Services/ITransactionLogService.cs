using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface ITransactionLogService
    {     
        public ITransactionLog CreateEntity();

        Task SaveAsync(ITransactionLog log);
    }
}
