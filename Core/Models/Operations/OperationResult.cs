namespace Core.Models.Operations
{
    public abstract class OperationResult
    {
        /// <summary>
        ///     Indicates if the operation completed successfully.
        /// </summary>
        public bool Success { get; set; } = true;

        public IList<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

        public string GetErrorsAsString()
        {
            return string.Join(",", Errors.Select(e => e.ErrorMessage).ToArray());
        }
    }
}
