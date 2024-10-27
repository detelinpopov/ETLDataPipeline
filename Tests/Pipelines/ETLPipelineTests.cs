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
                    new() { Id = 1, TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m, Customer = new CustomerModel { Id = 1, Name = "Customer 1" } },
                    new() { Id = 2, TransactionDate = new DateTime(2023, 10, 16), Amount = 99.75m, Customer = new CustomerModel { Id = 2, Name = "Customer 2" } }
                }
            });

            var apiDataSourceMock = new Mock<IDataSource<ExtractTransactionsResult>>();
            apiDataSourceMock.Setup(e => e.ExtractAsync()).ReturnsAsync(new ExtractTransactionsResult
            {
                Transactions = new List<TransactionModel>()
                {
                    new() { Id = 1, TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m, Customer = new CustomerModel { Id = 3, Name = "Customer 3" } },
                    new() { Id = 4, TransactionDate = new DateTime(2023, 10, 17), Amount = 55.30m, Customer = new CustomerModel { Id = 4, Name = "Customer 4" } }
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
            Assert.IsTrue(result.Transactions.Any(t => t.Customer.Name == "Customer 1" && t.Amount == 100.50m));
        }

        [TestMethod]
        public async Task Pipeline_ReturnsValidResult_WhenErrorsInExtractTransactionsResult()
        {
            // Arrange
            var csvDataSourceMock = new Mock<IDataSource<ExtractTransactionsResult>>();
            csvDataSourceMock.Setup(e => e.ExtractAsync()).ReturnsAsync(new ExtractTransactionsResult
            {
               Errors = new List<ErrorModel> { new ErrorModel() { ErrorMessage = "Test Error" } }
            });

            var apiDataSourceMock = new Mock<IDataSource<ExtractTransactionsResult>>();
            apiDataSourceMock.Setup(e => e.ExtractAsync()).ReturnsAsync(new ExtractTransactionsResult
            {
                Transactions = new List<TransactionModel>()
                {
                    new() { Id = 1, TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m, Customer = new CustomerModel { Id = 3, Name = "Customer 3" } },
                    new() { Id = 4, TransactionDate = new DateTime(2023, 10, 17), Amount = 55.30m, Customer = new CustomerModel { Id = 4, Name = "Customer 4" } }
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
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(1, result.Transactions.Count);
        }
    }
}
