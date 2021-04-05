using System.Threading.Tasks;
using GraphQL;
using Logic.ScheduleGenerators.RoundRobin.Cyclic;
using Logic.Types.RoundRobin;
using Logic.Types.Exceptions;
using GraphQL.Types;

namespace Schema.Query
{
    public class GenerateScheduleQuery : IQuery<RoundRobinSchedule>
    {
        private readonly ICyclicGenerator _scheduleGenerator;

        public GenerateScheduleQuery(ICyclicGenerator scheduleGenerator)
        {
            _scheduleGenerator = scheduleGenerator;
        }

        public async Task<RoundRobinSchedule> ExecuteAsync(ResolveFieldContext<object> context)
        {
            int roundCount = context.GetArgument<int>("roundCount");
            int participantCount = context.GetArgument<int>("participantCount");

            try
            {
                return await _scheduleGenerator.GenerateSchedule(roundCount, participantCount);
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
