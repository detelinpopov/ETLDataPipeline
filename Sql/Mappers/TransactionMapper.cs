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
            transaction.Amount = model.Amount;
            transaction.TransactionDate = model.TransactionDate;
            transaction.PaymentMethod = model.PaymentMethod;
            transaction.DataSource = model.DataSource;

            transaction.Customer = new Customer();
            transaction.Customer.Id = model.Customer.Id;
            transaction.Customer.Name = model.Customer.Name;
            transaction.Customer.CreatedDateUtc = DateTime.UtcNow;

            transaction.CreatedDateTimeUtc = DateTime.UtcNow;
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
