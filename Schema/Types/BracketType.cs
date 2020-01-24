using GraphQL.Types;
using Logic.Types.RoundRobin;

namespace Schema.Types
{
    public class BracketType : ObjectGraphType<RoundRobinBracket>
    {
        public BracketType()
        {
            Name = "Bracket";
            Description = "A mahjong bracket";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<BracketRoundType>>>>("rounds", "The rounds in the bracket");
        }
    }
}
