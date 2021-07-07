using BinarySearchable.Implementation;

namespace BinarySearchable
{
    public static class BinarySearchResult
    {
        public static IBinarySearchResult<T> Before<T>() => new BinarySearchResult<T> { Result = BinarySearchResultType.Before };
        public static IBinarySearchResult<T> After<T>() => new BinarySearchResult<T> { Result = BinarySearchResultType.After };
        public static IBinarySearchResult<T> Found<T>(T data) => new BinarySearchResult<T> { Result = BinarySearchResultType.Found, Data = data };
    }
}
