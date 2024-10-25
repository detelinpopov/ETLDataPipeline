using Interfaces.Sql.Entities;

namespace Interfaces.Core.DataSources
{
    public interface IDataSource
    {
        Task<IEnumerable<ITransaction>> ExtractAsync();
    }
}
