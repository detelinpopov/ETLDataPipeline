using Interfaces.Sql.Entities;

namespace Interfaces.Core.Services
{
    public interface ITransactionService
    {
        ITransaction CreateEntity();

        ICustomer CreateCustomerEntity();

        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
