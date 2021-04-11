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
            if (roundCount <= 0)
            {
                throw new ArgumentException("Invalid round count.");
            }

            IScheduleGenerator? generator = generators
                .Where(g => g.MaxRoundCount(participantCount) != null && roundCount <= g.MaxRoundCount(participantCount))
                .OrderBy(g => g.Specificity)
                .ThenByDescending(g => g.MaxRoundCount(participantCount))
                .FirstOrDefault();
            if (generator == null)
            {
                throw new ArgumentException("No schedule can be found.");
            }

            return generator.GenerateSchedule(participantCount, roundCount);
        }
    }
}
