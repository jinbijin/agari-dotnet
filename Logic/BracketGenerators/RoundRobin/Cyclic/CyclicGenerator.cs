using System.Threading.Tasks;
using Logic.Types.Bracket;
using Logic.Types.Exceptions;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public class CyclicGenerator : ICyclicGenerator
    {
        private readonly ICyclicSeedGenerator _cyclicSeedGenerator;
        public CyclicGenerator(ICyclicSeedGenerator cyclicSeedGenerator)
        {
            _cyclicSeedGenerator = cyclicSeedGenerator;
        }

        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        public Task<Bracket> GenerateBracket(int roundCount, int participantCount)
        {
            throw new System.NotImplementedException();
        }
    }
}
