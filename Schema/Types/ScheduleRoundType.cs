using GraphQL.Types;
using Logic.Types.RoundRobin;

namespace Schema.Types
{
    public class ScheduleRoundType : ObjectGraphType<RoundRobinRound>
    {
        public ScheduleRoundType()
        {
            Name = "ScheduleRound";
            Description = "A round in a mahjong schedule";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<ScheduleGameType>>>>("games", "The games in the round");
        }
    }
}
