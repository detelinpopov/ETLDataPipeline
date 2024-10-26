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
            transaction.CustomerName = model.CustomerName;
            transaction.Amount = model.Amount;
            transaction.TransactionDate = model.TransactionDate;
            transaction.DataSource = model.DataSource;

            if (model.PaymentDetails != null)
            {
                transaction.PaymentDetails = transactionService.CreatePaymentDetailsEntity();
                transaction.PaymentDetails.PaymentMethod = model.PaymentDetails.PaymentMethod;
                transaction.PaymentDetails.PaymentCompletedDate = model.PaymentDetails.PaymentCompletedDate;
                transaction.PaymentDetails.CreatedDateUtc = DateTime.UtcNow;
            }

            return transaction;
        }
    }
}
