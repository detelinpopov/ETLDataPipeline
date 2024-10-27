using Core.DataSources;
using Core.Models;
using Core.Pipelines;
using Core.Services;
using Core.TransformationRules;
using Interfaces.Core.Services;
using Interfaces.Core.Transformations;
using Interfaces.Sql.Entities;
using Interfaces.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sql.Context;
using Sql.Repositories;
using Core.Mappers;
using Interfaces.Core;
using Core.Models.Operations;
using Core.Enums;

class PipelineTestConsole
{
    static async Task Main(string[] args)
    {
        IServiceProvider serviceProvider = RegisterServices();
        var transactionLogService = serviceProvider.GetService<ITransactionLogService>();

        // Create CSV file for testing purposes
        var csvFilePath = "test_transactions.csv";
        var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                         "1,100,2024-10-15,CreditCard,1,Customer CSV 11\n" +
                         "2,56,2024-10-16,DigitalWallet,456,Customer CSV 2\n" +
                         "3,233,2024-01-02,DebitCard,78,Customer CSV 3";

        await File.WriteAllTextAsync(csvFilePath, csvContent);

        try
        {          
            // Define data sources         
            var csvDataSource = new CsvDataSource(csvFilePath);
            var apiDataSource = new ApiDataSource();

            // Define transformation rules
            var transformationRules = new List<ITransformationRule<TransactionModel>>
            {
                new RemoveDuplicatesRule(),
                new FilterByMinAmountRule(100.0m),
                new FilterByDateRule(DateTime.Now.AddMonths(-36))
            };

            // Create ETL pipeline
            var pipeline = new EtlPipeline(new IDataSource<ExtractTransactionsResult>[] { csvDataSource, apiDataSource }, transformationRules);

            // Run the pipeline
            var result = await pipeline.RunAsync();

            var transactionService = serviceProvider.GetService<ITransactionService>();
            
            var transactionsToSave = new List<ITransaction>();
            foreach (var transactionModel in result.Transactions.Where(r => r != null))
            {
                transactionsToSave.Add(transactionModel.ToTransaction(transactionService));
            }

            await transactionService.SaveAsync(transactionsToSave);

            // Output results
            Console.WriteLine($"{result.Transactions.Count} out of {result.Transactions.Count + result.Errors.Count} transactions processed successfully!");

            if(result.Errors.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{result.Errors.Count} error occured during the operation:");
                Console.WriteLine();
                int errorIndex = 1;
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error {errorIndex}: {error.ErrorMessage}.");

                    var log = transactionLogService.CreateEntity();
                    log.LogType = LogType.Error.ToString();
                    log.Message = error.ErrorMessage;
                    log.Severity = 5;
                    await transactionLogService.SaveAsync(log);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Successfully processed transactions: ");
            foreach (var transaction in result.Transactions)
            {
                Console.WriteLine();
                Console.WriteLine($"Customer Name: {transaction.Customer.Name}, Amount: {transaction.Amount}, Payment Method: {transaction.PaymentMethod}, Date: {transaction.TransactionDate}");               
            }
        }
        catch (Exception ex)
        {
            var log = transactionLogService.CreateEntity();
            log.LogType = LogType.Error.ToString();
            log.Message = ex.Message;
            log.Severity = 5;
            await transactionLogService.SaveAsync(log);
        }
        finally
        {
            // Clean up
            File.Delete(csvFilePath);
        }
    }

    private static IServiceProvider RegisterServices()
    {
        var services = new ServiceCollection();

        // Set up configuration sources.
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(AppContext.BaseDirectory))
            .AddJsonFile("appsettings.json", optional: false);

        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("DataPipelineConnectionString");

        services.AddDbContext<DataPipelineDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionLogRepository, TransactionLogRepository>();

        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITransactionLogService, TransactionLogService>();
       
        return services.BuildServiceProvider();
    }
}