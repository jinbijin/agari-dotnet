using Logic.Types.Bracket;

namespace Logic.BracketGenerators.RoundRobin.Toroidal
{
    public interface IToroidalGenerator
    {
        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        Bracket GenerateBracket(int roundCount, int participantCount);
    }
}
