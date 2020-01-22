using System.Threading.Tasks;
using Logic.Types.RoundRobin;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public interface ICyclicGenerator
    {
        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        Task<RoundRobinBracket> GenerateBracket(int roundCount, int participantCount);
    }
}
