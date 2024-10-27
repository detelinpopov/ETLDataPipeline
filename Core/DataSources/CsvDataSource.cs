using Core.Enums;
using Core.Models;
using Core.Models.Operations;
using Interfaces.Core;

namespace Core.DataSources
{
    public class CsvDataSource : IDataSource<ExtractTransactionsResult>
    {
        private readonly string _filePath;

        public CsvDataSource(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<ExtractTransactionsResult> ExtractAsync()
        {
            var convertedTransactionsResult = new ExtractTransactionsResult();
            using (var reader = new StreamReader(_filePath))
            {
                await reader.ReadLineAsync(); // Skip header
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = await reader.ReadLineAsync();
                        var values = line.Split(',');
                        var paymentMethod = Enum.TryParse(values[4], out PaymentMethod paymentMethodParsed);

                        var csvEntryToTransaction = new TransactionModel
                        {
                            Id = int.Parse(values[0]),
                            Amount = decimal.Parse(values[2]),
                            TransactionDate = DateTime.Parse(values[3]),
                            DataSource = DataSource.CSV.ToString(),
                            PaymentMethod = paymentMethodParsed,
                            Customer = new CustomerModel 
                            { 
                                Name = values[1] 
                            }
                        };

                        convertedTransactionsResult.Transactions.Add(csvEntryToTransaction);
                    }
                    catch (Exception ex)
                    {
                        convertedTransactionsResult.Errors.Add(new ErrorModel { ErrorMessage = ex.Message, ErrorType = ErrorType.ConvertEntryToModel });
                    }
                }
            }

            return convertedTransactionsResult;
        }
    }
}
