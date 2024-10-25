using Interfaces.Sql.Entities;
namespace Interfaces.Core.Transformations
{
    public interface ITransformationRule
    {
        IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> transactions);
    }
}
