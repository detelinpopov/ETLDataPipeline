using Core.Models;
using Core.Models.Operations;
using Core.Pipelines;
using Core.TransformationRules;
using Interfaces.Core;
using Interfaces.Core.Transformations;
using Moq;

namespace Tests.Pipelines
{
    [TestClass]
    public class ETLPipelineTests
    {
        [TestMethod]
        public async Task Pipeline_ProcessesAllDataCorrectly()
        {
            // Arrange
            var csvDataSourceMock = new Mock<IDataSource<ExtractTransactionsResult>>();
            csvDataSourceMock.Setup(e => e.ExtractAsync()).ReturnsAsync(new ExtractTransactionsResult
            {
                Transactions = new List<TransactionModel>()
                {
                    new() { Id = 1, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                    new() { Id = 2, CustomerName = "Jane Smith", TransactionDate = new DateTime(2023, 10, 16), Amount = 99.75m }
                }
            });

            var apiDataSourceMock = new Mock<IDataSource<ExtractTransactionsResult>>();
            apiDataSourceMock.Setup(e => e.ExtractAsync()).ReturnsAsync(new ExtractTransactionsResult
            {
                Transactions = new List<TransactionModel>()
                {
                    new() { Id = 1, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                    new() { Id = 4, CustomerName = "Alice Brown", TransactionDate = new DateTime(2023, 10, 17), Amount = 55.30m }
                 }
            });

            var dataSources = new List<IDataSource<ExtractTransactionsResult>> { csvDataSourceMock.Object, apiDataSourceMock.Object };
            var transformationRules = new List<ITransformationRule<TransactionModel>>
            {
                new RemoveDuplicatesRule(),
                new FilterByMinAmountRule(100)
            };

            var pipeline = new EtlPipeline(dataSources, transformationRules);

            // Act
            var result = await pipeline.RunAsync();

            // Assert
            Assert.AreEqual(1, result.Transactions.Count); // Duplicates removed. Id is used for comparison.
            Assert.IsTrue(result.Transactions.Any(t => t.CustomerName == "John Doe" && t.Amount == 100.50m));
        }
    }
}
