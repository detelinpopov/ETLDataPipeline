using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface ITransactionRepository
    {
        ITransaction CreateEntity();

        IPaymentDetails CreatePaymentDetailsEntity();

        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
