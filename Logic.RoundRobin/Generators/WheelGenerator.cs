using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.RoundRobin.Generators
{
    internal class WheelGenerator : IScheduleGenerator
    {
        public Specificity Specificity => Specificity.Generic;

        public int? MaxRoundCount(int participantCount)
        {
            int maxRounds = MaxRounds(participantCount);
            return maxRounds == 0 ? null : maxRounds;
        }

        public async Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount)
        {
            return new RoundRobinSchedule(await GenerateRounds(participantCount).Take(roundCount).ToListAsync());
        }

        private static int MaxRounds(int participantCount)
        {
            int tableCount = participantCount / 4;
            if (tableCount % 3 == 0)
            {
                return tableCount / 3;
            }

            return tableCount % 2 == 0 ? tableCount / 2 : tableCount;
        }

        private static async IAsyncEnumerable<RoundRobinRound> GenerateRounds(int participantCount)
        {
            int tableCount = participantCount / 4;
            for (int i = 0; i < tableCount; i++)
            {
                yield return new RoundRobinRound(Enumerable.Range(0, tableCount)
                    .Select(j => new RoundRobinGame(new List<int>
                        {
                            j,
                            tableCount + ((j + i) % tableCount),
                            2 * tableCount + ((j + 2 * i) % tableCount),
                            3 * tableCount + ((j + 3 * i) % tableCount)
                        })).ToList());
            }
        }
    }
}
