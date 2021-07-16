using Logic.Common.Random;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.RoundRobin.Generators
{
    internal class WheelGenerator : IScheduleGenerator
    {
        public Specificity Specificity => Specificity.Generic;

        public IReadOnlyCollection<string> RandomnessSources { get; } = new List<string>
        {
            "Based on a fixed schedule.",
            "Participant numbers are randomized.",
            "Table numbers are randomized.",
            "Round selection from the schedule is randomized."
        };

        public int? MaxRoundCount(int participantCount)
        {
            int maxRounds = MaxRounds(participantCount);
            return maxRounds == 0 ? null : maxRounds;
        }

        public async Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount)
        {
            return new RoundRobinSchedule(await GenerateRounds(participantCount).Take(roundCount).ToListAsync(), RandomnessSources);
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
            IList<int> shuffle = Enumerable.Range(0, participantCount).Shuffle();

            int tableCount = participantCount / 4;
            foreach (int i in Enumerable.Range(0, tableCount).Shuffle())
            {
                yield return new RoundRobinRound(Enumerable.Range(0, tableCount)
                    .Shuffle()
                    .Select(j => new RoundRobinGame(new List<int>
                        {
                            shuffle[j],
                            shuffle[tableCount + ((j + i) % tableCount)],
                            shuffle[2 * tableCount + ((j + 2 * i) % tableCount)],
                            shuffle[3 * tableCount + ((j + 3 * i) % tableCount)]
                        }.OrderBy(value => value).ToList())).ToList());
            }
        }
    }
}
