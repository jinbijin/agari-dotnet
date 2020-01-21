using System.Threading.Tasks;
using Logic.Types.Bracket;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public interface ICyclicGenerator
    {
        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        Task<Bracket> GenerateBracket(int roundCount, int participantCount);
    }
}
