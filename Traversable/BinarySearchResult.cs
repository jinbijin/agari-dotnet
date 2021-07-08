namespace Traversable
{
    public interface IBinarySearchResult<T>
    {
        BinarySearchResultType Result { get; }
        T Data { get; }
    }

    public enum BinarySearchResultType
    {
        Found,
        BeforeCurrent,
        AfterCurrent
    }

    public static class BinarySearchResult
    {
        public static IBinarySearchResult<T> BeforeCurrent<T>() => new BinarySearchResult<T> { Result = BinarySearchResultType.BeforeCurrent };
        public static IBinarySearchResult<T> AfterCurrent<T>() => new BinarySearchResult<T> { Result = BinarySearchResultType.AfterCurrent };
        public static IBinarySearchResult<T> Found<T>(T data) => new BinarySearchResult<T> { Result = BinarySearchResultType.Found, Data = data };
    }

    internal readonly struct BinarySearchResult<T> : IBinarySearchResult<T>
    {
        public readonly BinarySearchResultType Result { get; init; }
        public readonly T Data { get; init; }
    }
}
