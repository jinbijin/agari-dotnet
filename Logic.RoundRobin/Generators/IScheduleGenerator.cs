using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.RoundRobin.Generators
{
    internal interface IScheduleGenerator
    {
        /// <summary>
        /// The maximum number of rounds that the schedule generator can generate for `playerCount` number of players.
        /// </summary>
        int? MaxRoundCount(int participantCount);

        /// <summary>
        /// Generate a schedule
        /// </summary>
        Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount);
    }
}
