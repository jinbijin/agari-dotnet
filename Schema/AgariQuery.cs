using GraphQL.Types;
using Schema.Query;
using Schema.Types;

namespace Schema
{
    public class AgariQuery : ObjectGraphType
    {
        public AgariQuery(
            GenerateScheduleQuery generateScheduleQuery)
        {
            Name = "Query";
            FieldAsync<NonNullGraphType<ScheduleType>>("generateSchedule",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "roundCount" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "participantCount" }
                ),
                resolve: async ctx => await generateScheduleQuery.ExecuteAsync(ctx));
        }
    }
}
