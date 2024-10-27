using Core.Enums;
using Core.Models;
using Core.Models.Operations;
using Interfaces.Core;

namespace Core.DataSources
{
    public class ApiDataSource : IDataSource<ExtractTransactionsResult>
    {
        public async Task<ExtractTransactionsResult> ExtractAsync()
        {
            var result = new ExtractTransactionsResult();

            // Mock API call, replace with real API call logic
            await Task.Delay(100); // Simulating network latency

            var transactions = new List<TransactionModel>
                {
                    new()
                    {
                        Id = 101,
                        Amount = 120.50m,
                        TransactionDate = DateTime.Now,
                        PaymentMethod = PaymentMethod.CreditCard,
                        DataSource = DataSource.API.ToString(),
                        Customer = new CustomerModel
                        {
                           Name = "Test Customer 1",
                        }
                    },
                    new()
                    {
                        Id = 102,
                        Amount = 99.10m,
                        TransactionDate = DateTime.Now.AddDays(-7),
                        PaymentMethod = PaymentMethod.DigitalWallet,
                        DataSource = DataSource.API.ToString(),
                        Customer = new CustomerModel
                        {
                           Name = "Test Customer 2",
                        }
                    },
                    new()
                    {
                        Id = 103,
                        Amount = 3000m,
                        TransactionDate = DateTime.Now.AddMonths(-5),
                        PaymentMethod = PaymentMethod.BankTransfer,
                        DataSource = DataSource.API.ToString(),
                        Customer = new CustomerModel
                        {
                           Name = "Test Customer 3",
                        }
                    },
                    new()
                    {
                        Id = 104,
                        Amount = 5m,
                        TransactionDate = DateTime.Now.AddDays(-19),
                        PaymentMethod = PaymentMethod.Cryptocurrency,
                        DataSource = DataSource.API.ToString(),
                        Customer = new CustomerModel
                        {
                           Name = "Test Customer 4",
                        }
                    },
                };

            result.Transactions = transactions;
            return result;
        }
    }
}
