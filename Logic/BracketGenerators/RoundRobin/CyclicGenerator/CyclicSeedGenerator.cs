using System.Collections.Generic;

namespace Logic.BracketGenerators.RoundRobin.CyclicGenerator
{
    public class CyclicSeedGenerator : ICyclicSeedGenerator
    {
        /// <exception cref="SeedNotFoundException"/>
        public IEnumerable<(int, int)> GenerateSeed(int count, int modulus)
        {
            throw new System.NotImplementedException();
        }
    }
}
