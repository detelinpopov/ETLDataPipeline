using Interfaces.Core.Transformations;
using Interfaces.Sql.Entities;

namespace Core.TransformationRules
{
    public class RemoveDuplicatesRule : ITransformationRule
    {
        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> transactions)
        {
            return transactions.Distinct();
        }
    }
}
