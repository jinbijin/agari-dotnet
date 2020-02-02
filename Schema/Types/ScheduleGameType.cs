using GraphQL.Types;
using Logic.Types.RoundRobin;

namespace Schema.Types
{
    public class ScheduleGameType : ObjectGraphType<RoundRobinGame>
    {
        public ScheduleGameType()
        {
            Name = "ScheduleGame";
            Description = "A game in a mahjong schedule";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<IntGraphType>>>>("participantNrs", "The numbers of the participants in the game");
        }
    }
}
