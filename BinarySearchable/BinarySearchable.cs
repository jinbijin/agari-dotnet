using BinarySearchable.Implementation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BinarySearchable
{
    public static class BinarySearchable
    {
        public static IBinarySearchable<int> Range(int start, int count) => new RangeBinarySearchable(start, count);

        [return: MaybeNull]
        public static TDst Find<TSrc, TDst>(this IBinarySearchable<TSrc> binarySearchable, Func<TSrc, IBinarySearchResult<TDst>> evaluator)
        {
            var shouldContinue = true;
            var binarySearcher = binarySearchable.GetBinarySearcher();

            while (shouldContinue)
            {
                var result = evaluator(binarySearcher.Current);
                switch (result.Result)
                {
                    case BinarySearchResultType.Found:
                        return result.Data;
                    case BinarySearchResultType.Before:
                        shouldContinue = binarySearcher.MoveBefore();
                        break;
                    case BinarySearchResultType.After:
                        shouldContinue = binarySearcher.MoveAfter();
                        break;
                }
            }

            return default;
        }
    }
}
