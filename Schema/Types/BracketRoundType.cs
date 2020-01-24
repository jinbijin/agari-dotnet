using GraphQL.Types;
using Logic.Types.RoundRobin;

namespace Schema.Types
{
    public class BracketRoundType : ObjectGraphType<RoundRobinRound>
    {
        public BracketRoundType()
        {
            Name = "BracketRound";
            Description = "A round in a mahjong bracket";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<BracketGameType>>>>("games", "The games in the round");
        }
    }
}
