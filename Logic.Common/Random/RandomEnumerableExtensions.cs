using System.Collections.Generic;
using System.Linq;

namespace Logic.Common.Random
{
    public static class RandomEnumerableExtensions
    {
        private static System.Random _random = new System.Random();

        public static IList<T> Shuffle<T>(this IEnumerable<T> source)
        {
            List<T> list = new();
            foreach (var (item, index) in source.Select((item, index) => (item, index)))
            {
                var insertIndex = _random.Next(index);
                list.Insert(insertIndex, item);
            }

            return list;
        }
    }
}
