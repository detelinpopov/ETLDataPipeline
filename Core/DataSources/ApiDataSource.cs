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
                        Id = 101, CustomerName = "Test Customer 1",
                        Amount = 120.50m,
                        TransactionDate = DateTime.Now,
                        DataSource = DataSource.API.ToString(),
                        PaymentDetails = new PaymentDetailsModel
                        {
                            PaymentMethod = PaymentMethod.BankTransfer.ToString(),
                            PaymentCompletedDate = DateTime.Now,
                        }
                    },
                   new() 
                   {
                        Id = 102, CustomerName = "Test Customer 2",
                        Amount = 99.50m,
                        TransactionDate = DateTime.Now.AddHours(-7),
                        DataSource = DataSource.API.ToString(),
                        PaymentDetails = new PaymentDetailsModel
                        {
                            PaymentMethod = PaymentMethod.CreditCard.ToString(),
                            PaymentCompletedDate = DateTime.Now.AddHours(-8),
                        }
                    },
                    new() 
                    {
                        Id = 103, CustomerName = "Test Customer 3",
                        Amount = 2500m,
                        TransactionDate = DateTime.Now.AddMonths(-3),
                        DataSource = DataSource.API.ToString(),
                        PaymentDetails = new PaymentDetailsModel
                        {
                            PaymentMethod = PaymentMethod.DigitalWallet.ToString(),
                            PaymentCompletedDate = DateTime.Now.AddMonths(-3),
                        }
                    }
                };

            result.Transactions = transactions;
            return result;
        }
    }
}
