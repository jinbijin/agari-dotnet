using System.Threading.Tasks;

namespace Logic.RoundRobin
{
    public interface IRoundRobinScheduleGenerator
    {
        Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount);
        Task<bool> ValidateGenerateScheduleRequest(int participantCount, int roundCount);
    }
}
