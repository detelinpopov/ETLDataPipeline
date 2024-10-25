using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface ITransactionService
    {
        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
