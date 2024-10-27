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
            var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                             "1,100,2024-10-15,CreditCard,1,Customer CSV 1\n" +
                             "2,56,2024-10-16,DigitalWallet,456,Customer CSV 2\n" +
                             "3,233,2024-01-02,DebitCard,77,Customer CSV 3";


            await File.WriteAllTextAsync(csvFilePath, csvContent);

            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var transactions = (await csvDataSource.ExtractAsync()).Transactions.ToList();

            // Assert
            Assert.AreEqual(3, transactions.Count);
            Assert.AreEqual("Customer CSV 1", transactions[0].Customer.Name);
            Assert.AreEqual(100m, transactions[0].Amount);

            // Clean up
            File.Delete(csvFilePath);
        }
    }
}
