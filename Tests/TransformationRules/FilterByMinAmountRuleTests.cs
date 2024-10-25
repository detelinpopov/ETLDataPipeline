using Core.Models;
using Core.TransformationRules;

namespace Tests.TransformationRules
{
    [TestClass]
    public class FilterByMinAmountRuleTests
    {
        [TestMethod]
        public void FilterByMinAmountRule_FiltersSuccessfully()
        {
            // Arrange
            var transactions = new List<TransactionModel>
            {
                new TransactionModel { Id = 1, CustomerName = "Customer Less Than 100", TransactionDate = new DateTime(2023, 10, 15), Amount = 99m },
                new TransactionModel { Id = 2, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                new TransactionModel { Id = 3, CustomerName = "Jane Smith", TransactionDate = new DateTime(2023, 10, 16), Amount = 200.75m }
            };

            var rule = new FilterByMinAmountRule(100);

            // Act
            var result = rule.Apply(transactions).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(r => r.CustomerName == "John Doe"));
            Assert.IsTrue(result.Any(r => r.CustomerName == "Jane Smith"));
            Assert.IsFalse(result.Any(r => r.CustomerName == "Customer Less Than 100"));
        }
    }
}
