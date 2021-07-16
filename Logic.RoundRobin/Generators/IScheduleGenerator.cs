using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.RoundRobin.Generators
{
    internal interface IScheduleGenerator
    {
        /// <summary>
        /// The specificity of the generator. Is used in prioritizing the schedule generator used.
        /// </summary>
        Specificity Specificity { get; }

        /// <summary>
        /// The maximum number of rounds that the schedule generator can generate for `playerCount` number of players.
        /// </summary>
        int? MaxRoundCount(int participantCount);

        /// <summary>
        /// The list of sources of randomness for this schedule generator.
        /// </summary>
        IReadOnlyCollection<string> RandomnessSources { get; }

        /// <summary>
        /// Generate a schedule. Parameters are already validated.
        /// </summary>
        Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount);
    }
}
