using Logic.Types.Bracket;

namespace Logic.BracketGenerators.RoundRobin.CyclicGenerator
{
    public interface ICyclicGenerator
    {
        Bracket GenerateBracket(int roundCount, int participantCount);
    }
}
