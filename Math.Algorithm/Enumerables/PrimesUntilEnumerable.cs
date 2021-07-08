using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Math.Algorithm.Enumerables
{
    internal class PrimesUntilEnumerable : IEnumerable<int>
    {
        private readonly int _max;

        public PrimesUntilEnumerable(int max)
        {
            _max = max;
        }

        public IEnumerator<int> GetEnumerator() => new PrimesUntilEnumerator(_max);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class PrimesUntilEnumerator : IEnumerator<int>
    {
        private readonly int _max;
        private List<int> _primes;

        public int Current { get; private set; }

        object IEnumerator.Current => Current;

        public PrimesUntilEnumerator(int max)
        {
            _max = max;

            Reset();
        }

        public bool MoveNext()
        {
            for (int i = Current + 1; i <= _max; i++)
            {
                if (_primes.All(prime => i % prime != 0))
                {
                    Current = i;
                    if (i * i <= _max) { _primes.Add(i); }
                    return true;
                }
            }

            return false;
        }

        [MemberNotNull(nameof(_primes))]
        public void Reset()
        {
            _primes = new List<int>();
            Current = 1;
        }

        public void Dispose()
        {
        }
    }
}
