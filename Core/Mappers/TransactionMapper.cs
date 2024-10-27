using Core.Models;
using Interfaces.Core.Services;
using Interfaces.Sql.Entities;

namespace Core.Mappers
{
    public static class TransactionMapper
    {
        public static ITransaction ToTransaction(this TransactionModel model, ITransactionService transactionService)
        {
            ITransaction transaction = transactionService.CreateEntity();
            transaction.Id = model.Id;
            transaction.Amount = model.Amount;
            transaction.TransactionDate = model.TransactionDate;
            transaction.DataSource = model.DataSource;
            transaction.PaymentMethod = model.PaymentMethod.ToString();

            transaction.Customer = transactionService.CreateCustomerEntity();
            transaction.Customer.Id = model.Customer.Id;
            transaction.Customer.Name = model.Customer.Name;
            transaction.Customer.CreatedDateUtc = DateTime.UtcNow;

            return transaction;
        }
    }
}
