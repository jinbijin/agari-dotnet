using System.Collections.Generic;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public interface ICyclicSeedGenerator
    {
        /// <exception cref="SeedNotFoundException"/>
        IEnumerable<(int, int)> GenerateSeed(int count, int modulus);
    }
}
