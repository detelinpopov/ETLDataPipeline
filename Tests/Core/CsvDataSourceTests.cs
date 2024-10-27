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

        [TestMethod]
        public async Task CsvExtractor_ReturnsCorrectResult_WhenInvalidTransactionId()
        {
            // Arrange
            var csvFilePath = "test_transactions.csv";
            var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                             "InvalidId,100,2024-10-15,CreditCard,1,Customer CSV 1\n" +
                             "2,56,2024-10-16,DigitalWallet,456,Customer CSV 2\n" +
                             "3,233,2024-01-02,DebitCard,77,Customer CSV 3";


            await File.WriteAllTextAsync(csvFilePath, csvContent);
            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var result = await csvDataSource.ExtractAsync();

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any());
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(2, result.Transactions.Count);
            Assert.AreEqual("Customer CSV 2", result.Transactions[0].Customer.Name);

            // Clean up
            File.Delete(csvFilePath);
        }

        [TestMethod]
        public async Task CsvExtractor_ReturnsCorrectResult_WhenInvalidTransactionAmount()
        {
            // Arrange
            var csvFilePath = "test_transactions.csv";
            var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                             "1,23,2024-10-15,CreditCard,1,Customer CSV 1\n" +
                             "2,INVALID_AMOUNT,2024-10-16,DigitalWallet,456,Customer CSV 2\n" +
                             "3,233,2024-01-02,DebitCard,77,Customer CSV 3";


            await File.WriteAllTextAsync(csvFilePath, csvContent);
            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var result = await csvDataSource.ExtractAsync();

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any());
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(2, result.Transactions.Count);

            // Clean up
            File.Delete(csvFilePath);
        }

        [TestMethod]
        public async Task CsvExtractor_ReturnsCorrectResult_WhenInvalidTransactionDate()
        {
            // Arrange
            var csvFilePath = "test_transactions.csv";
            var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                             "1,23,2024-10-15,CreditCard,1,Customer CSV 1\n" +
                             "2,122,INVALID_DATE,DigitalWallet,456,Customer CSV 2\n" +
                             "3,233,2024-01-02,DebitCard,77,Customer CSV 3";


            await File.WriteAllTextAsync(csvFilePath, csvContent);
            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var result = await csvDataSource.ExtractAsync();

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any());
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(2, result.Transactions.Count);

            // Clean up
            File.Delete(csvFilePath);
        }

        [TestMethod]
        public async Task CsvExtractor_ReturnsCorrectResult_WhenInvalidTransactionPaymentMethod()
        {
            // Arrange
            var csvFilePath = "test_transactions.csv";
            var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                             "1,23,2024-10-15,CreditCard,1,Customer CSV 1\n" +
                             "2,122,2024-01-01,DigitalWallet,456,Customer CSV 2\n" +
                             "3,233,2024-01-02,INVALID_PAYMENT_METHOD,77,Customer CSV 3";


            await File.WriteAllTextAsync(csvFilePath, csvContent);
            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var result = await csvDataSource.ExtractAsync();

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any());
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(2, result.Transactions.Count);

            // Clean up
            File.Delete(csvFilePath);
        }

        [TestMethod]
        public async Task CsvExtractor_ReturnsCorrectResult_WhenInvalidCustomerId()
        {
            // Arrange
            var csvFilePath = "test_transactions.csv";
            var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                             "1,23,2024-10-15,CreditCard,INVALID_CUSTOMER_ID,Customer CSV 1\n" +
                             "2,122,2024-01-01,DigitalWallet,456,Customer CSV 2\n" +
                             "3,233,2024-01-02,CreditCard,77,Customer CSV 3";


            await File.WriteAllTextAsync(csvFilePath, csvContent);
            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var result = await csvDataSource.ExtractAsync();

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any());
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(2, result.Transactions.Count);

            // Clean up
            File.Delete(csvFilePath);
        }

        [TestMethod]
        public async Task CsvExtractor_ReturnsCorrectResult_When2InvalidFields()
        {
            // Arrange
            var csvFilePath = "test_transactions.csv";
            var csvContent = "Id,Amount,TransactionDate,PaymentMethod,CustomerId,CustomerName\n" +
                             "1,23,2024-10-15,CreditCard,INVALID_CUSTOMER_ID,Customer CSV 1\n" +
                             "2,122,2024-01-01,DigitalWallet,456,Customer CSV 2\n" +
                             "INVALID_ID,233,2024-01-02,CreditCard,77,Customer CSV 3";


            await File.WriteAllTextAsync(csvFilePath, csvContent);
            var csvDataSource = new CsvDataSource(csvFilePath);

            // Act
            var result = await csvDataSource.ExtractAsync();

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any());
            Assert.AreEqual(2, result.Errors.Count);
            Assert.AreEqual(1, result.Transactions.Count);

            // Clean up
            File.Delete(csvFilePath);
        }
    }
}
