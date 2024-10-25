using Interfaces.Sql.Entities;
using Sql.Entities;

namespace Sql.Mappers
{
    public static class TransactionMapper
    {
        public static Transaction ToDbTransaction(this ITransaction model)
        {
            var transaction = new Transaction();
            transaction.CustomerName = model.CustomerName;
            transaction.Amount = model.Amount;
            transaction.TransactionDate = model.TransactionDate;
            transaction.DataSource = model.DataSource;
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
