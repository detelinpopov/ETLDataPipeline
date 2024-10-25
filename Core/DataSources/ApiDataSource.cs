using Core.Enums;
using Core.Models;
using Interfaces.Core.DataSources;

namespace Core.DataSources
{
    public class ApiDataSource : IDataSource<TransactionModel>
    {
        public async Task<IEnumerable<TransactionModel>> ExtractAsync()
        {
            // Mock API call, replace with real API call logic
            await Task.Delay(100); // Simulating network latency

            return new List<TransactionModel>
                {
                    new TransactionModel { Id = 101, CustomerName = "Test Customer 1", Amount = 120.50m, TransactionDate = DateTime.Now, DataSource = DataSource.API.ToString() },
                    new TransactionModel { Id = 102, CustomerName = "Test Customer 2", Amount = 399.75m, TransactionDate = DateTime.Now.AddDays(-1), DataSource = DataSource.API.ToString() },
                    new TransactionModel { Id = 103, CustomerName = "Important Customer", Amount = 666m, TransactionDate = DateTime.Now.AddDays(-100), DataSource = DataSource.API.ToString() }
                };
        }
    }
}
