using GraphQL.Types;
using Logic.Types.RoundRobin;

namespace Schema.Types
{
    public class BracketGameType : ObjectGraphType<RoundRobinGame>
    {
        public BracketGameType()
        {
            Name = "BracketGame";
            Description = "A game in a mahjong bracket";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<IntGraphType>>>>("participantNrs", "The numbers of the participants in the game");
        }
    }
}
