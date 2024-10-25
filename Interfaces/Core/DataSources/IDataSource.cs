namespace Interfaces.Core.DataSources
{
    public interface IDataSource<T>
    {
        Task<IEnumerable<T>> ExtractAsync();
    }
}
