using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Logic.BracketGenerators.RoundRobin.Cyclic;
using Logic.Types.Bracket;
using Logic.Types.Exceptions;

namespace Schema.Query
{
    public class GenerateBracketQuery : IQuery<Bracket>
    {
        private readonly ICyclicGenerator _bracketGenerator;

        public GenerateBracketQuery(ICyclicGenerator bracketGenerator)
        {
            _bracketGenerator = bracketGenerator;
        }

        public async Task<Bracket> ExecuteAsync(ResolveFieldContext<object> context)
        {
            int roundCount = context.GetArgument<int>("roundCount");
            int participantCount = context.GetArgument<int>("participantCount");

            try
            {
                return await _bracketGenerator.GenerateBracket(roundCount, participantCount);
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
