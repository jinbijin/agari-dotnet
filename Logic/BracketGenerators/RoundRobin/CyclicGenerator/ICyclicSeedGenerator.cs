using System.Collections.Generic;

namespace Logic.BracketGenerators.RoundRobin.CyclicGenerator
{
    public interface ICyclicSeedGenerator
    {
        IEnumerable<(int, int)> GenerateSeed(int roundCount, int participantCount);
    }
}
