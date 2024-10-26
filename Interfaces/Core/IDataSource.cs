namespace Interfaces.Core
{
    public interface IDataSource<T>
    {
        Task<T> ExtractAsync();
    }
}
