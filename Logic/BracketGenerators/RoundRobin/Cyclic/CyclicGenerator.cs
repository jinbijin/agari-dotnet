using Logic.Types.Bracket;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public class CyclicGenerator : ICyclicGenerator
    {
        private readonly ICyclicSeedGenerator _cyclicSeedGenerator;

        public CyclicGenerator(ICyclicSeedGenerator cyclicSeedGenerator)
        {
            _cyclicSeedGenerator = cyclicSeedGenerator;
        }

        public Bracket GenerateBracket(int roundCount, int participantCount)
        {
            throw new System.NotImplementedException();
        }
    }
}
