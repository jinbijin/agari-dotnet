namespace Traversable.Implementation
{
    internal class RangeBinarySearcher : IBinarySearcher<int>
    {
        private int _start;
        private int _end;

        public int Current => (_start + _end) / 2;

        public RangeBinarySearcher(int start, int count)
        {
            _start = start;
            _end = start + count - 1;
        }

        public bool MoveBefore()
        {
            if (Current - _start <= 0)
            {
                return false;
            }

            _end = Current - 1;
            return true;
        }

        public bool MoveAfter()
        {
            if (_end - Current <= 0)
            {
                return false;
            }

            _start = Current + 1;
            return true;
        }
    }
}
