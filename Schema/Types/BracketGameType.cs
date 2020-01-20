using GraphQL.Types;
using Logic.Types.Bracket;

namespace Schema.Types
{
    public class BracketGameType : ObjectGraphType<BracketGame>
    {
        public BracketGameType()
        {
            Name = "BracketGame";
            Description = "A game in a mahjong bracket";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<IntGraphType>>>>("participantNrs", "The numbers of the participants in the game");
        }
    }
}
