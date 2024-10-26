using Core.DataSources;

namespace Tests.Core
{
    [TestClass]
    public class ApiDataSourceTests
    {
        [TestMethod]
        public async Task MockApiExtractor_ShouldExtractDataFromMockApi()
        {
            // Arrange
            var mockApiExtractor = new ApiDataSource();

            // Mock the response to return hardcoded JSON
            var mockResult = await mockApiExtractor.ExtractAsync();

            // Assert
            Assert.IsNotNull(mockResult);
            Assert.AreEqual("Test Customer 1", mockResult.First().CustomerName);
        }
    }
}
