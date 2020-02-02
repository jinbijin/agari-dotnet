using GraphQL.Types;
using Logic.Types.RoundRobin;

namespace Schema.Types
{
    public class ScheduleType : ObjectGraphType<RoundRobinSchedule>
    {
        public ScheduleType()
        {
            Name = "Schedule";
            Description = "A mahjong schedule";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<ScheduleRoundType>>>>("rounds", "The rounds in the schedule");
        }
    }
}
