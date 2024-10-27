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
                new TransactionModel { Id = 1, TransactionDate = new DateTime(2024, 10, 15), Amount = 99m, Customer = new CustomerModel { Name = "Customer 1" } },
                new TransactionModel { Id = 2, TransactionDate = new DateTime(2022, 10, 15), Amount = 99m, Customer = new CustomerModel { Name = "Customer 2" } },
                new TransactionModel { Id = 3, TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m, Customer = new CustomerModel { Name = "Customer 3" } },
                new TransactionModel { Id = 4, TransactionDate = new DateTime(2024, 10, 16), Amount = 200.75m, Customer = new CustomerModel { Name = "Customer 4" } }
            };

            var rule = new FilterByDateRule(new DateTime(2024, 1, 1));

            // Act
            var result = rule.Apply(transactions).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(r => r.Customer.Name == "Customer 1"));
            Assert.IsTrue(result.Any(r => r.Customer.Name == "Customer 4"));
            Assert.IsFalse(result.Any(r => r.Customer.Name == "Customer 2"));
            Assert.IsFalse(result.Any(r => r.Customer.Name == "Customer 3"));
        }
    }
}
