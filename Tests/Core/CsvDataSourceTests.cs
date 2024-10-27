using Core.DataSources;

namespace Tests.Core
{
    [TestClass]
    public class CsvDataSourceTests
    {
        [TestMethod]
        public async Task CsvExtractor_ExtractsDataFromCsv()
        {
            // Arrange
            var csvFilePath = "test_transactions.csv";
            var csvContent = "Id,CustomerName,Amount,TransactionDate,PaymentMethod\n" +
                             "1,John Doe,100,2024-10-15,CreditCard\n" +
                             "2,Jane Smith,200,2024-10-16,DigitalWallet";

            await File.WriteAllTextAsync(csvFilePath, csvContent);

            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var transactions = (await csvDataSource.ExtractAsync()).Transactions.ToList();

            // Assert
            Assert.AreEqual(2, transactions.Count);
            Assert.AreEqual("John Doe", transactions[0].Customer.Name);
            Assert.AreEqual(100m, transactions[0].Amount);

            // Clean up
            File.Delete(csvFilePath);
        }
    }
}
