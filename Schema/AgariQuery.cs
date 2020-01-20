using GraphQL.Types;
using Schema.Query;
using Schema.Types;

namespace Schema
{
    public class AgariQuery : ObjectGraphType
    {
        public AgariQuery(
            GenerateBracketQuery generateBracketQuery)
        {
            Name = "Query";
            FieldAsync<NonNullGraphType<BracketType>>("generateBracket",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "roundCount" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "participantCount" }
                ),
                resolve: async ctx => await generateBracketQuery.ExecuteAsync(ctx));
        }
    }
}
