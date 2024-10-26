namespace Core.Models.Operations
{
    /// <summary>
    /// Contains details about the converted transactions that come fron the data source.
    /// </summary>
    public class ExtractTransactionsResult : OperationResult
    {
        /// <summary>
        /// Contains a list of successfully extracted transactions.
        /// </summary>
        public IList<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();

        public IList<ErrorModel> Errors { get; set; }
    }
}
