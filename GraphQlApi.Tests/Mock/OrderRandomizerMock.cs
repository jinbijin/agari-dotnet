using System.Collections.Generic;
using Logic.Randomizer;

namespace GraphQlApi.Tests.Mock
{
    public class OrderRandomizerMock : IOrderRandomizer
    {
        public IEnumerable<T> RandomizeOrder<T>(IEnumerable<T> ts)
        {
            return ts;
        }
    }
}
