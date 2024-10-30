namespace Interfaces.Core.Transformations
{
    public interface ITransformationRule<T>
    {
        IEnumerable<T> Apply(IEnumerable<T> collectionToTransform);
    }
}
