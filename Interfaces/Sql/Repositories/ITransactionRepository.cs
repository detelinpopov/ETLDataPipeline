using Interfaces.Sql.Entities;

namespace Interfaces.Sql.Repositories
{
    public interface ITransactionRepository
    {
        ITransaction CreateEntity();

        ICustomer CreateCustomerEntity();

        Task SaveAsync(IEnumerable<ITransaction> transactions);
    }
}
