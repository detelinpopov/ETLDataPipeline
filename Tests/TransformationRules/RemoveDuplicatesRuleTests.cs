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
                new() { Id = 1, TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m, Customer = new CustomerModel { Name = "Customer 1" } },
                new() { Id = 1, TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m, Customer = new CustomerModel { Name = "Customer 1" } },
                new() { Id = 2, TransactionDate = new DateTime(2023, 10, 16), Amount = 200.75m, Customer = new CustomerModel { Name = "Customer 2" } }
            };

            var rule = new RemoveDuplicatesRule();

            // Act
            var result = rule.Apply(transactions).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(r => r.Customer.Name == "Customer 1"));
            Assert.IsTrue(result.Any(r => r.Customer.Name == "Customer 2"));
        }
    }
}
