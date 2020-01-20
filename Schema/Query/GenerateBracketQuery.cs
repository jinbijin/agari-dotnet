using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Logic.BracketGenerators.RoundRobin.Cyclic;
using Logic.Types.Bracket;

namespace Schema.Query
{
    public class GenerateBracketQuery : IQuery<Bracket>
    {
        private readonly ICyclicGenerator _bracketGenerator;

        public GenerateBracketQuery(ICyclicGenerator bracketGenerator)
        {
            _bracketGenerator = bracketGenerator;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Bracket> ExecuteAsync(ResolveFieldContext<object> context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int roundCount = context.GetArgument<int>("roundCount");
            int participantCount = context.GetArgument<int>("participantCount");

            try
            {
                return _bracketGenerator.GenerateBracket(roundCount, participantCount);
            }
            catch (SeedNotFoundException ex)
            {
                throw new ExecutionError(ex.Message, ex);
            }
            catch (InvalidParameterException ex)
            {
                throw new ExecutionError(ex.Message, ex);
            }
        }
    }
}
