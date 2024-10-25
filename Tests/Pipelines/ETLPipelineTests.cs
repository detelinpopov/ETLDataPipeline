using Core.Models;
using Core.Pipelines;
using Core.TransformationRules;
using Interfaces.Core.DataSources;
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
            var csvDataSourceMock = new Mock<IDataSource<TransactionModel>>();
            csvDataSourceMock.Setup(e => e.ExtractAsync()).ReturnsAsync(new List<TransactionModel>
            {
                new TransactionModel { Id = 1, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                new TransactionModel { Id = 2, CustomerName = "Jane Smith", TransactionDate = new DateTime(2023, 10, 16), Amount = 99.75m }
            });

            var apiDataSourceMock = new Mock<IDataSource<TransactionModel>>();
            apiDataSourceMock.Setup(e => e.ExtractAsync()).ReturnsAsync(new List<TransactionModel>
            {
                new TransactionModel { Id = 3, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                new TransactionModel { Id = 4, CustomerName = "Alice Brown", TransactionDate = new DateTime(2023, 10, 17), Amount = 55.30m }
            });

            var dataSources = new List<IDataSource<TransactionModel>> { csvDataSourceMock.Object, apiDataSourceMock.Object };
            var transformationRules = new List<ITransformationRule<TransactionModel>>
            {
                new RemoveDuplicatesRule(),
                new FilterByMinAmountRule(100)
            };

            var pipeline = new EtlPipeline(dataSources, transformationRules);

            // Act
            var result = (await pipeline.RunAsync()).ToList();

            // Assert
            Assert.AreEqual(1, result.Count); // Duplicates removed
            Assert.IsTrue(result.Any(t => t.CustomerName == "John Doe" && t.Amount == 100.50m));           
        }
    }
}
