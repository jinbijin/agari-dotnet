using Traversable.Implementation;

namespace Traversable
{
    public static class BinarySearchResult
    {
        public static IBinarySearchResult<T> BeforeCurrent<T>() => new BinarySearchResult<T> { Result = BinarySearchResultType.BeforeCurrent };
        public static IBinarySearchResult<T> AfterCurrent<T>() => new BinarySearchResult<T> { Result = BinarySearchResultType.AfterCurrent };
        public static IBinarySearchResult<T> Found<T>(T data) => new BinarySearchResult<T> { Result = BinarySearchResultType.Found, Data = data };
    }
}
