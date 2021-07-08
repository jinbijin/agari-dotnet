using System.Collections;
using System.Collections.Generic;

namespace Math.Algorithm.Enumerable
{
    internal class RangeFromUntilLogEnumerable : IEnumerable<int>
    {
        private readonly int _start;
        private readonly int _powerBase;
        private readonly int _max;

        public RangeFromUntilLogEnumerable(int start, int powerBase, int max)
        {
            _start = start;
            _powerBase = powerBase;
            _max = max;
        }

        public IEnumerator<int> GetEnumerator() => new RangeFromUntilLogEnumerator(_start, _powerBase, _max);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class RangeFromUntilLogEnumerator : IEnumerator<int>
    {
        private readonly int _start;
        private readonly int _powerBase;
        private readonly int _max;

        private int _nextPower;

        public int Current { get; private set; }

        object IEnumerator.Current => Current;

        public RangeFromUntilLogEnumerator(int start, int powerBase, int max)
        {
            _start = start;
            _powerBase = powerBase;
            _max = max;

            Reset();
        }

        public bool MoveNext()
        {
            if (_nextPower > _max) { return false; }

            Current++;
            _nextPower *= _powerBase;
            return true;
        }

        public void Reset()
        {
            Current = _start - 1;
            _nextPower = _powerBase.ToPowerTruncated(_start, _max);
        }

        public void Dispose()
        {
        }
    }
}
