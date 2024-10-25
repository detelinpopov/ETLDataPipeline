using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface ITransactionRepository
    {
        ITransaction CreateEntity();

        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
