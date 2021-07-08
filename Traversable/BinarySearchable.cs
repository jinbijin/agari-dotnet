using Traversable.Implementation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Traversable
{
    public interface IBinarySearchable<T>
    {
        IBinarySearcher<T> GetBinarySearcher();
    }

    public interface IBinarySearcher<T>
    {
        T Current { get; }
        bool MoveBefore();
        bool MoveAfter();
    }

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
                    case BinarySearchResultType.BeforeCurrent:
                        shouldContinue = binarySearcher.MoveAfter();
                        break;
                    case BinarySearchResultType.AfterCurrent:
                        shouldContinue = binarySearcher.MoveBefore();
                        break;
                }
            }

            return default;
        }
    }
}
