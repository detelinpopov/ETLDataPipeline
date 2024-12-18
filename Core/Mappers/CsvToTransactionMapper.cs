﻿using Core.Enums;
using Core.Models;
using Core.Models.Operations;

namespace Core.Mappers
{
    public static class CsvToTransactionMapper
    {
        public static ConvertCsvToTransactionModelResult CsvLineToTransactionModel(this string csvLine)
        {
            var result = new ConvertCsvToTransactionModelResult();
            try
            {               
                var csvColumns = csvLine.Split(',');
                var csvModel = ValidateCsvColumns(csvColumns, result);

                if (!result.Success)
                {
                    return result;
                }

                var transactionModel = new TransactionModel
                {
                    Id = csvModel.TransactionId,
                    Amount = csvModel.TransactionAmount,
                    TransactionDate = csvModel.TransactionDate,
                    DataSource = DataSource.CSV.ToString(),
                    PaymentMethod = csvModel.PaymentMethod,
                    Customer = new CustomerModel
                    {
                        Id = csvModel.CustomerId,
                        Name = csvColumns[5]
                    }
                };

                result.ConvertedTransactionModel = transactionModel;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = ex.Message });
                return result;
            }
        }  
        
        private static ParsedCsvModel ValidateCsvColumns(string[] csvColumns, ConvertCsvToTransactionModelResult result)
        {
            var csvModel = new ParsedCsvModel();

            if (!int.TryParse(csvColumns[0], out int transactionId))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Id in column 1 of the CSV file: '{csvColumns[0]}'!" });
            }
            else
            {
                csvModel.TransactionId = transactionId;
            }

            if (!decimal.TryParse(csvColumns[1], out decimal transactionAmount))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Amount in column 2 of the CSV file: '{csvColumns[1]}'!" });
            }
            else
            {
                csvModel.TransactionAmount = transactionAmount;
            }

            if (!DateTime.TryParse(csvColumns[2], out DateTime transactionDate))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Date in column 3 of the CSV file: '{csvColumns[2]}'!" });
            }
            else
            {
                csvModel.TransactionDate = transactionDate;
            }

            if (!Enum.TryParse(csvColumns[3], out PaymentMethod paymentMethodParsed))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Transaction Payment Method in column 4 of the CSV file: '{csvColumns[3]}'!" });
            }
            else
            {
                csvModel.PaymentMethod = paymentMethodParsed;
            }

            if (!int.TryParse(csvColumns[4], out int customerId))
            {
                result.Errors.Add(new ErrorModel { ErrorMessage = $"Invalid Customer Id in column 1 of the CSV file: '{csvColumns[4]}'!" });
            }
            else
            {
                csvModel.CustomerId = customerId;
            }

            return csvModel;
        }
    }
}
