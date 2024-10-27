using Core.Enums;
using Core.Models;
using Core.Models.Operations;

namespace Core.Mappers
{
    public static class CsvToTransactionMapper
    {
        public static ConvertCsvToTransactionModelResult CsvLineToTransactionModel(this string csvLine)
        {
            var result = new ConvertCsvToTransactionModelResult();

            var csvColumns = csvLine.Split(',');
            if (!int.TryParse(csvColumns[0], out int transactionId))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Id in column 1 of the CSV file: '{csvColumns[0]}'!" });
            }
            if (!decimal.TryParse(csvColumns[1], out decimal transactionAmount))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Amount in column 2 of the CSV file: '{csvColumns[1]}'!" });
            }
            if (!DateTime.TryParse(csvColumns[2], out DateTime transactionDate))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Date in column 3 of the CSV file: '{csvColumns[2]}'!" });
            }
            if (!Enum.TryParse(csvColumns[3], out PaymentMethod paymentMethodParsed))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Payment Method in column 4 of the CSV file: '{csvColumns[3]}'!" });
            }
            if (!int.TryParse(csvColumns[4], out int customerId))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Customer Id in column 1 of the CSV file: '{csvColumns[4]}'!" });
            }

            if (!result.Success)
            {
                return result;
            }
          
            var transactionModel = new TransactionModel
            {
                Id = transactionId,
                Amount = transactionAmount,
                TransactionDate = transactionDate,
                DataSource = DataSource.CSV.ToString(),
                PaymentMethod = paymentMethodParsed,
                Customer = new CustomerModel
                {
                    Id = customerId,
                    Name = csvColumns[5]
                }
            };

            result.ConvertedTransactionModel = transactionModel;
            return result;
        }       
    }
}
