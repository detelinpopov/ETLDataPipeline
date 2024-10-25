using Core.Models;
using Core.TransformationRules;

namespace Tests.TransformationRules
{
    [TestClass]
    public class FilterByDateRuleTests
    {
        [TestMethod]
        public void FilterByDateRuleTests_FiltersSuccessfully()
        {
            // Arrange
            var transactions = new List<TransactionModel>
            {
                new TransactionModel { Id = 1, CustomerName = "Customer 1", TransactionDate = new DateTime(2024, 10, 15), Amount = 99m },
                new TransactionModel { Id = 2, CustomerName = "Customer 2", TransactionDate = new DateTime(2022, 10, 15), Amount = 99m },
                new TransactionModel { Id = 3, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                new TransactionModel { Id = 4, CustomerName = "Jane Smith", TransactionDate = new DateTime(2024, 10, 16), Amount = 200.75m }
            };

            var rule = new FilterByDateRule(new DateTime(2024, 1, 1));

            // Act
            var result = rule.Apply(transactions).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(r => r.CustomerName == "Customer 1"));
            Assert.IsTrue(result.Any(r => r.CustomerName == "Jane Smith"));
            Assert.IsFalse(result.Any(r => r.CustomerName == "Customer 2"));
            Assert.IsFalse(result.Any(r => r.CustomerName == "John Doe"));
        }
    }
}
