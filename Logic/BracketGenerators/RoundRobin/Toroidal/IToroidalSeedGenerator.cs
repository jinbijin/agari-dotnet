using System.Collections.Generic;

namespace Logic.BracketGenerators.RoundRobin.Toroidal
{
    public interface IToroidalSeedGenerator
    {
        /// <exception cref="SeedNotFoundException"/>
        IEnumerable<(int, int)> GenerateSeed(int count, int modulus);
    }
}
