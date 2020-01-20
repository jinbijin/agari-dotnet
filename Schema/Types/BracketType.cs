using GraphQL.Types;
using Logic.Types.Bracket;

namespace Schema.Types
{
    public class BracketType : ObjectGraphType<Bracket>
    {
        public BracketType()
        {
            Name = "Bracket";
            Description = "A mahjong bracket";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<BracketRoundType>>>>("rounds", "The rounds in the bracket");
        }
    }
}
