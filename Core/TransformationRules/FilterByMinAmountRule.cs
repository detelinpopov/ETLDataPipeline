using Interfaces.Core.Transformations;
using Interfaces.Sql.Entities;

namespace Core.TransformationRules
{
    public class FilterByMinAmountRule : ITransformationRule
    {
        private readonly decimal _minAmount;

        public FilterByMinAmountRule(decimal minAmount)
        {
            _minAmount = minAmount;
        }

        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> transactions)
        {
            return transactions.Where(t => t.Amount >= _minAmount);
        }
    }
}
