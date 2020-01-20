using Logic.Types.Bracket;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public interface ICyclicGenerator
    {
        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        Bracket GenerateBracket(int roundCount, int participantCount);
    }
}
