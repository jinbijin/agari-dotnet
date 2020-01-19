using System.Collections.Generic;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public interface ICyclicSeedGenerator
    {
        IEnumerable<(int, int)> GenerateSeed(int count, int modulus);
    }
}
