using Interfaces.Sql.Entities;
using Sql.Entities;

namespace Sql.Mappers
{
    public static class TransactionMapper
    {
        public static Transaction ToDbTransaction(this ITransaction model)
        {
            var transaction = new Transaction();
            transaction.Id = model.Id;
            transaction.CustomerName = model.CustomerName;
            transaction.Amount = model.Amount;
            transaction.TransactionDate = model.TransactionDate;
            transaction.DataSource = model.DataSource;

            if(model.PaymentDetails != null)
            {
                transaction.PaymentDetails = new PaymentDetails();
                transaction.PaymentDetails.PaymentMethod = model.PaymentDetails.PaymentMethod;
                transaction.PaymentDetails.PaymentCompletedDate = model.PaymentDetails.PaymentCompletedDate;
                transaction.PaymentDetails.CreatedDateUtc = DateTime.UtcNow;
            }

            return transaction;
        }

        public static List<Transaction> ToListOfDbTransactions(this IEnumerable<ITransaction> transactionsToMap)
        {
            var mappedTransactions = new List<Transaction>();
            foreach (var transactionToMap in transactionsToMap) 
            {
                var mappedTransaction = transactionToMap.ToDbTransaction();
                mappedTransactions.Add(mappedTransaction);
            }

            return mappedTransactions;
        }
    }
}
