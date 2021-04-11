using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.RoundRobin.Generators
{
    internal class AffineF4Generator : IScheduleGenerator
    {
        public Specificity Specificity => Specificity.Special;

        public int? MaxRoundCount(int participantCount) => participantCount == 16 ? 5 : null;

        public async Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount)
        {
            return new RoundRobinSchedule(await GenerateRounds().Take(roundCount).ToListAsync());
        }

        private static async IAsyncEnumerable<RoundRobinRound> GenerateRounds()
        {
            yield return new RoundRobinRound(new List<RoundRobinGame>
            {
                new RoundRobinGame(new List<int> { 0, 1, 2, 3 }),
                new RoundRobinGame(new List<int> { 4, 5, 6, 7 }),
                new RoundRobinGame(new List<int> { 8, 9, 10, 11 }),
                new RoundRobinGame(new List<int> { 12, 13, 14, 15 })
            });
            yield return new RoundRobinRound(new List<RoundRobinGame>
            {
                new RoundRobinGame(new List<int> { 0, 5, 10, 15 }),
                new RoundRobinGame(new List<int> { 1, 4, 11, 14 }),
                new RoundRobinGame(new List<int> { 2, 7, 8, 13 }),
                new RoundRobinGame(new List<int> { 3, 6, 9, 12 })
            });
            yield return new RoundRobinRound(new List<RoundRobinGame>
            {
                new RoundRobinGame(new List<int> { 0, 7, 9, 14 }),
                new RoundRobinGame(new List<int> { 1, 6, 8, 15 }),
                new RoundRobinGame(new List<int> { 2, 5, 11, 12 }),
                new RoundRobinGame(new List<int> { 3, 4, 10, 13 })
            });
            yield return new RoundRobinRound(new List<RoundRobinGame>
            {
                new RoundRobinGame(new List<int> { 0, 6, 11, 13 }),
                new RoundRobinGame(new List<int> { 1, 7, 10, 12 }),
                new RoundRobinGame(new List<int> { 2, 4, 9, 15 }),
                new RoundRobinGame(new List<int> { 3, 5, 8, 14 })
            });
            yield return new RoundRobinRound(new List<RoundRobinGame>
            {
                new RoundRobinGame(new List<int> { 0, 4, 8, 12 }),
                new RoundRobinGame(new List<int> { 1, 5, 9, 13 }),
                new RoundRobinGame(new List<int> { 2, 6, 10, 14 }),
                new RoundRobinGame(new List<int> { 3, 7, 11, 15 })
            });
        }
    }
}
