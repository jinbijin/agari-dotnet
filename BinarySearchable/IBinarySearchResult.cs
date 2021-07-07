namespace BinarySearchable
{
    public interface IBinarySearchResult<T>
    {
        BinarySearchResultType Result { get; }
        T Data { get; }
    }
}
