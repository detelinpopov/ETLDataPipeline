using Core.Enums;
using Core.Models;
using Interfaces.Core.DataSources;
using Interfaces.Sql.Entities;

namespace Core.DataSources
{
    public class CsvDataSource : IDataSource
    {
        private readonly string _filePath;

        public CsvDataSource(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IEnumerable<ITransaction>> ExtractAsync()
        {
            var transactions = new List<TransactionModel>();
            using (var reader = new StreamReader(_filePath))
            {
                await reader.ReadLineAsync(); // Skip header
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    transactions.Add(new TransactionModel
                    {
                        Id = int.Parse(values[0]),
                        CustomerName = values[1],
                        Amount = decimal.Parse(values[2]),
                        TransactionDate = DateTime.Parse(values[3]),
                        DataSource = DataSource.CSV.ToString(),
                    });
                }
            }

            return transactions;
        }
    }
}
