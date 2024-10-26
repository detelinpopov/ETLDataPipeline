namespace Interfaces.Core
{
    public interface IDataSource<T>
    {
        Task<IEnumerable<T>> ExtractAsync();
    }
}
