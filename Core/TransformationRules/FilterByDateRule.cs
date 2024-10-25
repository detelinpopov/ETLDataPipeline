using Core.Models;
using Interfaces.Core.Transformations;

namespace Core.TransformationRules
{
    public class FilterByDateRule : ITransformationRule<TransactionModel>
    {
        private readonly DateTime _fromDate;

        public FilterByDateRule(DateTime fromDate)
        {
            _fromDate = fromDate;
        }

        public IEnumerable<TransactionModel> Apply(IEnumerable<TransactionModel> transactions)
        {
            return transactions.Where(t => t.TransactionDate >= _fromDate);
        }
    }
}
