using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface ITransactionService
    {
        ITransaction CreateEntity();

        IPaymentDetails CreatePaymentDetailsEntity();

        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
