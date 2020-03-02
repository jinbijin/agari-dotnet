using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Randomizer
{
    public class OrderRandomizer : IOrderRandomizer
    {
        public OrderRandomizer()
        {
        }

        public IEnumerable<T> RandomizeOrder<T>(IEnumerable<T> ts)
        {
            Random random = new Random();
            var rs = ts.Select(t => (t, random.NextDouble())).ToList();
            return rs.OrderBy(r => r.Item2).Select(r => r.t);
        }
    }
}
