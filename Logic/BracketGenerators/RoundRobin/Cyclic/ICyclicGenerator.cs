using Logic.Types.Bracket;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public interface ICyclicGenerator
    {
        Bracket GenerateBracket(int roundCount, int participantCount);
    }
}
