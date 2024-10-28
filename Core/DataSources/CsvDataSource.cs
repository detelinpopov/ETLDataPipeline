using Core.Mappers;
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
                    var csvLine = await reader.ReadLineAsync();

                    var convertCsvToModelResult = CsvToTransactionMapper.CsvLineToTransactionModel(csvLine);
                    if (convertCsvToModelResult.Success)
                    {
                        convertedTransactionsResult.Transactions.Add(convertCsvToModelResult.ConvertedTransactionModel);
                    }
                    else
                    {
                        foreach (var error in convertCsvToModelResult.Errors)
                        {
                            convertedTransactionsResult.Errors.Add(error);
                        }
                    }
                }
            }

            return convertedTransactionsResult;
        }
    }
}
