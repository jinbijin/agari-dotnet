using Logic.Common;
using Logic.RoundRobin.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.RoundRobin.Implementations
{
    internal class RoundRobinScheduleGenerator : IRoundRobinScheduleGenerator
    {
        private readonly ICollection<IScheduleGenerator> generators;

        public RoundRobinScheduleGenerator(ICollection<IScheduleGenerator> generators)
        {
            this.generators = generators;
        }

        public Task<Response<RoundRobinSchedule, Error>> GenerateSchedule(int participantCount, int roundCount)
        {
            Response<IScheduleGenerator, Error> generatorResponse = ValidateParameters(participantCount, roundCount);
            return generatorResponse.Map(async generator => await generator.GenerateSchedule(participantCount, roundCount));
        }

        public async Task<Response<bool, Error>> ValidateGenerateScheduleRequest(int participantCount, int roundCount)
        {
            Response<IScheduleGenerator, Error> generatorResponse = ValidateParameters(participantCount, roundCount);
            return generatorResponse.Map(generator => true);
        }

        private Response<IScheduleGenerator, Error> ValidateParameters(int participantCount, int roundCount)
        {
            if (roundCount <= 0)
            {
                return Response<IScheduleGenerator, Error>.Failure(new RoundCountError());
            }

            if (participantCount <= 0 || participantCount % 4 != 0)
            {
                return Response<IScheduleGenerator, Error>.Failure(new ParticipantCountError());
            }

            if (3 * roundCount > participantCount - 1)
            {
                return Response<IScheduleGenerator, Error>.Failure(new RoundRobinScheduleNotAvailableError());
            }

            IScheduleGenerator? generator = generators
                .Where(g => g.MaxRoundCount(participantCount) != null && roundCount <= g.MaxRoundCount(participantCount))
                .OrderBy(g => g.Specificity)
                .ThenByDescending(g => g.MaxRoundCount(participantCount))
                .FirstOrDefault();
            return generator == null ? Response<IScheduleGenerator, Error>.Failure(new RoundRobinScheduleNotFoundError()) : Response<IScheduleGenerator, Error>.Success(generator);
        }
    }
}
