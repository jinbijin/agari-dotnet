using System.Collections.Generic;

namespace Math.Algorithm.Enumerables
{
    public static class Ranges
    {
        /// <summary>
        /// Returns the range from <paramref name="start"/> up to (and including)
        /// the largest integer n such that <paramref name="powerBase"/> raised to the power n is at most <paramref name="max"/>.
        /// </summary>
        public static IEnumerable<int> FromUntilLog(int start, int powerBase, int max) => new RangeFromUntilLogEnumerable(start, powerBase, max);

        /// <summary>
        /// Returns an enumerable of all primes up to (and including) <paramref name="max"/>.
        /// </summary>
        public static IEnumerable<int> PrimesUntil(int max) => new PrimesUntilEnumerable(max);
    }
}
