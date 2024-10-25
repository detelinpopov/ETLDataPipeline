using Core.Models;
using Core.TransformationRules;

namespace Tests.TransformationRules
{
    [TestClass]
    public class RemoveDuplicatesRuleTests
    {
        [TestMethod]
        public void RemoveDuplicatesRule_RemovesDuplicates()
        {
            // Arrange
            var transactions = new List<TransactionModel>
            {
                new TransactionModel { Id = 1, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                new TransactionModel { Id = 2, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                new TransactionModel { Id = 3, CustomerName = "Jane Smith", TransactionDate = new DateTime(2023, 10, 16), Amount = 200.75m }
            };

            var rule = new RemoveDuplicatesRule();

            // Act
            var result = rule.Apply(transactions).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(r => r.CustomerName == "John Doe"));
            Assert.IsTrue(result.Any(r => r.CustomerName == "Jane Smith"));
        }
    }
}
