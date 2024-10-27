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
                new TransactionModel { Id = 1, TransactionDate = new DateTime(2023, 10, 15), Amount = 99m, Customer = new CustomerModel {Id = 1,  Name = "Customer Less Than 100" } },
                new TransactionModel { Id = 2, TransactionDate = new DateTime(2023, 10, 15), Amount = 100.50m, Customer = new CustomerModel {Id = 2,  Name = "Customer 2" } },
                new TransactionModel { Id = 3, TransactionDate = new DateTime(2023, 10, 16), Amount = 200.75m, Customer = new CustomerModel {Id = 3,  Name = "Customer 3" }}
            };

            var rule = new FilterByMinAmountRule(100);

            // Act
            var result = rule.Apply(transactions).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(r => r.Customer.Name == "Customer 2"));
            Assert.IsTrue(result.Any(r => r.Customer.Name == "Customer 3"));
            Assert.IsFalse(result.Any(r => r.Customer.Name == "Customer Less Than 100"));
        }
    }
}
