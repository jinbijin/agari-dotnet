namespace BinarySearchable.Implementation
{
    internal class RangeBinarySearchable : IBinarySearchable<int>
    {
        private readonly int _start;
        private readonly int _count;

        public RangeBinarySearchable(int start, int count)
        {
            _start = start;
            _count = count;
        }

        public IBinarySearcher<int> GetBinarySearcher() => new RangeBinarySearcher(_start, _count);
    }
}
