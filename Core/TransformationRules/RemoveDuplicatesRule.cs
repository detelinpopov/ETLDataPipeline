using Core.Models;
using Interfaces.Core.Transformations;

namespace Core.TransformationRules
{
    public class RemoveDuplicatesRule : ITransformationRule<TransactionModel>
    {
        public IEnumerable<TransactionModel> Apply(IEnumerable<TransactionModel> transactions)
        {
            return transactions.Distinct();
        }
    }
}
