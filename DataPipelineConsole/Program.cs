using Core.DataSources;
using Core.Pipelines;
using Core.Services;
using Core.TransformationRules;
using Interfaces.Core.DataSources;
using Interfaces.Core.Services;
using Interfaces.Core.Transformations;
using Interfaces.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sql.Context;
using Sql.Repositories;

class Program
{
    static async Task Main(string[] args)
    {
        IServiceProvider serviceProvider = RegisterServices();

        // Define data sources
        var csvSource = new CsvDataSource(@"C:\Users\Acer\source\repos\DataPipeline\DataPipelineConsole\TestFiles\sampleTransactions.csv");
        var apiSource = new ApiDataSource();

        // Define transformation rules
        var transformationRules = new List<ITransformationRule>
        {
            //new DateFormattingRule("dd/MM/yyyy"),
            new FilterByMinAmountRule(100.0m),
            new RemoveDuplicatesRule()
        };

        // Create ETL pipeline
        var pipeline = new EtlPipeline(new IDataSource[] { csvSource, apiSource }, transformationRules);

        // Run the pipeline
        var results = await pipeline.RunAsync();

        var transactionService = serviceProvider.GetService<ITransactionService>();
        await transactionService.SaveAsync(results);

        // Output results
        foreach (var transaction in results)
        {
            Console.WriteLine($"Transaction: {transaction.CustomerName}, Amount: {transaction.Amount}, Date: {transaction.TransactionDate}");
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