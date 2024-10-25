using Core.Models;
using Interfaces.Core.Transformations;

namespace Core.TransformationRules
{
    public class FilterByMinAmountRule : ITransformationRule<TransactionModel>
    {
        private readonly decimal _minAmount;

        public FilterByMinAmountRule(decimal minAmount)
        {
            _minAmount = minAmount;
        }

        public IEnumerable<TransactionModel> Apply(IEnumerable<TransactionModel> transactions)
        {
            return transactions.Where(t => t.Amount >= _minAmount);
        }
    }
}
