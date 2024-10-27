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

class Program
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
                         "3,233,2024-01-02,DebitCard,77,Customer CSV 3";

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
            foreach (var transaction in result.Transactions)
            {
                Console.WriteLine($"Transaction: {transaction.Customer.Name}, Amount: {transaction.Amount}, Date: {transaction.TransactionDate}");

                var log = transactionLogService.CreateEntity();
                log.LogType = "Message";
                log.Message = $"Transaction Created: Customer Name: {transaction.Customer.Name}, Amount: {transaction.Amount}, Date: {transaction.TransactionDate}";
                transactionLogService.SaveAsync(log);
            }
        }
        catch (Exception ex)
        {
            var log = transactionLogService.CreateEntity();
            log.LogType = "Error";
            log.Message = ex.Message;
            log.Severity = 5;
            transactionLogService.SaveAsync(log);
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