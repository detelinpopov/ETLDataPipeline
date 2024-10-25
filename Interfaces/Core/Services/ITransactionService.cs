using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface ITransactionService
    {
        ITransaction CreateEntity();

        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
