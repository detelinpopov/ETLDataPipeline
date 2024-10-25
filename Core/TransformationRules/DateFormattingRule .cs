using Interfaces.Core.Transformations;
using Interfaces.Sql.Entities;
using System.Globalization;

namespace Core.TransformationRules
{
    public class DateFormattingRule : ITransformationRule
    {
        private readonly string _targetDateFormat;

        public DateFormattingRule(string targetDateFormat)
        {
            _targetDateFormat = targetDateFormat;
        }

        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                var dateToString = transaction.TransactionDate.ToString();
                transaction.TransactionDate = DateTime.ParseExact(dateToString, _targetDateFormat, CultureInfo.InvariantCulture);
            }

            return transactions;
        }
    }
}
