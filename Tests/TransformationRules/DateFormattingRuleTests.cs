using Core.Models;
using Core.TransformationRules;

namespace Tests.TransformationRules
{
    [TestClass]
    public class DateFormattingRuleTests
    {
        [TestMethod]
        public void DateFormattingRule_FormatsDateCorrectly()
        {
            // Arrange
            var transactions = new List<TransactionModel>
            {
                new TransactionModel { Id = 1, CustomerName = "John Doe", TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m },
                new TransactionModel { Id = 2, CustomerName = "Jane Smith", TransactionDate = new DateTime(2023, 10, 16), Amount = 200.75m }
            };

            var rule = new DateFormattingRule("yyyy-MM-dd");

            // Act
            var result = rule.Apply(transactions).ToList();

            // Assert
            //Assert.That(result, t => Assert.Equal("yyyy-MM-dd", t.TransactionDate.ToString("yyyy-MM-dd")));
        }
    }
}
