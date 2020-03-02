using System.Collections.Generic;

namespace Logic.Randomizer
{
    public interface IOrderRandomizer
    {
        IEnumerable<T> RandomizeOrder<T>(IEnumerable<T> ts);
    }
}
