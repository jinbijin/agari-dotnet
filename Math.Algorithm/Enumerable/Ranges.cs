using System.Collections.Generic;

namespace Math.Algorithm.Enumerable
{
    public static class Ranges
    {
        public static IEnumerable<int> FromUntilLog(int start, int powerBase, int max) => new RangeFromUntilLogEnumerable(start, powerBase, max);
    }
}
