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

        public Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount)
        {
            IScheduleGenerator? generator = generators
                .Where(g => g.MaxRoundCount(participantCount) != null)
                .OrderByDescending(g => g.MaxRoundCount(participantCount))
                .FirstOrDefault();
            if (generator == null)
            {
                throw new ArgumentException("No schedule can be found.");
            }

            return generator.GenerateSchedule(participantCount, roundCount);
        }
    }
}
