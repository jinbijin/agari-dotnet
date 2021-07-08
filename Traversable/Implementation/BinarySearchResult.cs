namespace Traversable.Implementation
{
    internal readonly struct BinarySearchResult<T> : IBinarySearchResult<T>
    {
        public readonly BinarySearchResultType Result { get; init; }
        public readonly T Data { get; init; }
    }
}
