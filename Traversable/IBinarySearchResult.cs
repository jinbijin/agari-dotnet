namespace Traversable
{
    public interface IBinarySearchResult<T>
    {
        BinarySearchResultType Result { get; }
        T Data { get; }
    }
}
