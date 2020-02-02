using System.Threading.Tasks;
using Logic.Types.RoundRobin;

namespace Logic.ScheduleGenerators.RoundRobin.Cyclic
{
    public interface ICyclicGenerator
    {
        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        Task<RoundRobinSchedule> GenerateSchedule(int roundCount, int participantCount);
    }
}
