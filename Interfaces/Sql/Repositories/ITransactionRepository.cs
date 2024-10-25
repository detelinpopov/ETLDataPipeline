using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface ITransactionRepository
    {
        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
