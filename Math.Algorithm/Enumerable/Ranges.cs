using System.Collections.Generic;

namespace Math.Algorithm.Enumerable
{
    public static class Ranges
    {
        /// <summary>
        /// Returns the range from <paramref name="start"/> up to (and including)
        /// the largest integer n such that <paramref name="powerBase"/> raised to the power n is at most <paramref name="max"/>.
        /// </summary>
        public static IEnumerable<int> FromUntilLog(int start, int powerBase, int max) => new RangeFromUntilLogEnumerable(start, powerBase, max);
    }
}
