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

class Program
{
    static async Task Main(string[] args)
    {
        // Create CSV file for testing purposes
        var csvFilePath = "test_transactions.csv";
        var csvContent = "Id,CustomerName,Amount,TransactionDate\n" +
                         "1,John Doe,100,2024-10-15\n" +
                         "2,Jane Smith,200,2024-10-16";

        await File.WriteAllTextAsync(csvFilePath, csvContent);

        try
        {
            IServiceProvider serviceProvider = RegisterServices();

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
            var pipeline = new EtlPipeline(new IDataSource<TransactionModel>[] { csvDataSource, apiDataSource }, transformationRules);

            // Run the pipeline
            var results = await pipeline.RunAsync();

            var transactionService = serviceProvider.GetService<ITransactionService>();

            var transactionsToSave = new List<ITransaction>();
            foreach (var transactionModel in results.Where(r => r != null))
            {
                transactionsToSave.Add(transactionModel.ToTransaction(transactionService));
            }

            await transactionService.SaveAsync(transactionsToSave);

            // Output results
            foreach (var transaction in results)
            {
                Console.WriteLine($"Transaction: {transaction.CustomerName}, Amount: {transaction.Amount}, Date: {transaction.TransactionDate}");
            }
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
        services.AddScoped<ITransactionService, TransactionService>();
       
        return services.BuildServiceProvider();
    }
}