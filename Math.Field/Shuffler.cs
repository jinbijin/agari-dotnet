using System;
using System.Collections.Generic;

namespace Math.Field
{
    public static class Shuffler
    {
        public static readonly Random Random = new Random();

        public static List<TElement> Shuffle<TElement>(this List<TElement> list)
        {
            for (int n = list.Count; n >= 1; n--)
            {
                int index = Random.Next(n);
                TElement temp = list[index];
                list[index] = list[n];
                list[n] = temp;
            }

            return list;
        }
    }
}
